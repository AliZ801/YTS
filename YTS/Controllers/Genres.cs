using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTS.Models.ViewModels;
using YTS.Repository.IRepository;

namespace YTS.Controllers
{
    public class Genres : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminVM AVM { get; set; }

        public Genres(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddGenre(int? id)
        {
            AVM = new AdminVM() { Genres = new Models.Genres() };

            if(id != null)
            {
                AVM.Genres = _unitofWork.Genres.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenre()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Genres.Id == 0)
                {
                    _unitofWork.Genres.Add(AVM.Genres);
                }
                else
                {
                    _unitofWork.Genres.Update(AVM.Genres);
                }

                _unitofWork.Save();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(AVM);
            }
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            return Json(new { data = _unitofWork.Genres.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var gFromDb = _unitofWork.Genres.Get(id);

            if(gFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Genre Record!" });
            }

            _unitofWork.Genres.Remove(gFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Genre Record Deleted Successfully!" });
        }

        #endregion
    }
}
