using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using YTS.Models.ViewModels;
using YTS.Repository.IRepository;

namespace YTS.Controllers
{
    public class Movies : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public AdminVM AVM { get; set; }

        public Movies(IUnitofWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMovie(int? id)
        {
            AVM = new AdminVM()
            {
                Movies = new Models.Movies(),
                QualityList = _unitofWork.Quality.GetDropDownListForQuality(),
                GenresList = _unitofWork.Genres.GetDropDownListForGenres(),
                RatingsList = _unitofWork.Ratings.GetDropDownListForRatings()
            };

            if(id != null)
            {
                AVM.Movies = _unitofWork.Movies.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMovie()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if(AVM.Movies.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\Movies");
                    var extension = Path.GetExtension(files[0].FileName);

                    using(var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    AVM.Movies.ImageUrl = @"\Images\Movies\" + fileName + extension;

                    _unitofWork.Movies.Add(AVM.Movies);
                }
                else
                {
                    var mFromDb = _unitofWork.Movies.Get(AVM.Movies.Id);

                    if(files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"Images\Movies");
                        var extension_new = Path.GetExtension(files[0].FileName);
                        var imagePath = Path.Combine(webRootPath, mFromDb.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        AVM.Movies.ImageUrl = @"\Images\Movies\" + fileName + extension_new;
                    }
                    else
                    {
                        AVM.Movies.ImageUrl = mFromDb.ImageUrl;
                    }

                    _unitofWork.Movies.Update(AVM.Movies);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                AVM.QualityList = _unitofWork.Quality.GetDropDownListForQuality();
                AVM.GenresList = _unitofWork.Genres.GetDropDownListForGenres();
                AVM.RatingsList = _unitofWork.Ratings.GetDropDownListForRatings();

                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Movies.GetAll(includeProperties: "Quality,Genres,Ratings") });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var mFromDb = _unitofWork.Movies.Get(id);
            
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, mFromDb.ImageUrl.TrimStart('\\'));
            
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (mFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting Movie Rocord!" });
            }

            _unitofWork.Movies.Remove(mFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Movie Rocord Deleted Successfully!" });
        }

        #endregion
    }
}
