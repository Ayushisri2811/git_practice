using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebApp.Controllers
{
    using DoctorWebApp.Models;
    using DoctorWebApp.Repositories;
    using System.Web.Mvc;

    public class AppointmentController : Controller
    {
        public ActionResult Index()
        {
            return View(AppointmentRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentRepository.Add(appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        public ActionResult Edit(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                AppointmentRepository.Update(appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        public ActionResult Delete(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            AppointmentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}