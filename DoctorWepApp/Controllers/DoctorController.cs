using DoctorWepApp.Models;
using DoctorWepApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWepApp.Controllers
{

    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        // ✅ Dependency Injection
        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetAllDoctors());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (_service.AddDoctor(doctor))
            {
                TempData["Message"] = "Doctor added successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid data");
            return View(doctor);
        }

        public ActionResult Details(int id)
        {
            return View(_service.GetDoctorById(id));
        }

        public ActionResult Edit(int id)
        {
            return View(_service.GetDoctorById(id));
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            _service.UpdateDoctor(doctor);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_service.GetDoctorById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _service.DeleteDoctor(id);
            return RedirectToAction("Index");
        }
    }
}
