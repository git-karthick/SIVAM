using Microsoft.AspNetCore.Mvc;
using SIVAM.DAL;
using SIVAM.Models;

namespace SIVAM.Controllers
{
    public class SivamController : Controller
    {
        private readonly SivamDAL _dal;
        public SivamController(SivamDAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Sivam> school = new List<Sivam>();
            try
            {
                school = _dal.GetAll();
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
            }
            return View(school);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Sivam school)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMassage"] = "DATA IN INVALID";
                }
                bool result = _dal.Insert(school);
                if (!result)
                {
                    TempData["errorMassage"] = "unable to save data";
                    return View();
                }
                TempData["successMassage"] = "DATA SAVED";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Sivam sivam = _dal.GetById(id);
                if (sivam.Id == 0)
                {
                    TempData["errorMassage"] = $"data not found with id : {id}";
                    return RedirectToAction("Index");
                }
                return View(sivam);
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Sivam sivam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "data is invalid";
                    return View();
                }
                bool result = _dal.Update(sivam);
                if (!result)
                {
                    TempData["errorMassage"] = "unable to update data";
                    return View();
                }
                TempData["successMassage"] = "DATA UPDATED";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Sivam sivam = _dal.GetById(id);
                if (sivam.Id == 0)
                {
                    TempData["errorMassage"] = $"data not found with id : {id}";
                    return RedirectToAction("Index");
                }
                return View(sivam);
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Delete(Sivam sivam)
        {
            try
            {
                
                bool result = _dal.Delete(sivam.Id);
                if (!result)
                {
                    TempData["errorMassage"] = "unable to delete data";
                    return View();
                }
                TempData["successMassage"] = "DATA DELETED";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMassage"] = ex.Message;
                return View();
            }
        }
    }
}
