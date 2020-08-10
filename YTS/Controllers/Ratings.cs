using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTS.Models.ViewModels;
using YTS.Repository.IRepository;

namespace YTS.Controllers
{
    public class Ratings : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminVM AVM { get; set; }

        public Ratings(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRating(int? id)
        {
            AVM = new AdminVM() { Ratings = new Models.Ratings() };

            if(id != null)
            {
                AVM.Ratings = _unitofWork.Ratings.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRating()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Ratings.Id == 0)
                {
                    _unitofWork.Ratings.Add(AVM.Ratings);
                }
                else
                {
                    _unitofWork.Ratings.Update(AVM.Ratings);
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
            return Json(new { data = _unitofWork.Ratings.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var rFromDb = _unitofWork.Ratings.Get(id);

            if(rFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Rating Record!" });
            }

            _unitofWork.Ratings.Remove(rFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Rating Record Deleted Successfully!" });
        }

        #endregion
    }
}
