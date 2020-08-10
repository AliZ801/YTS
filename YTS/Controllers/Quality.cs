using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YTS.Models.ViewModels;
using YTS.Repository.IRepository;

namespace YTS.Controllers
{
    public class Quality : Controller
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public AdminVM AVM { get; set; }

        public Quality(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddQuality(int? id)
        {
            AVM = new AdminVM() { Quality = new Models.Quality() };

            if(id != null)
            {
                AVM.Quality = _unitofWork.Quality.Get(id.GetValueOrDefault());
            }

            return View(AVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuality()
        {
            if (ModelState.IsValid)
            {
                if(AVM.Quality.Id == 0)
                {
                    _unitofWork.Quality.Add(AVM.Quality);
                }
                else
                {
                    _unitofWork.Quality.Update(AVM.Quality);
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
            return Json(new { data = _unitofWork.Quality.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var qFromDb = _unitofWork.Quality.Get(id);

            if(qFromDb == null)
            {
                return Json(new { success = false, message = "Error Deleting Quality Recored!" });
            }

            _unitofWork.Quality.Remove(qFromDb);
            _unitofWork.Save();

            return Json(new { success = true, message = "Quality Recored Deleted Successfully!" });
        }

        #endregion
    }
}
