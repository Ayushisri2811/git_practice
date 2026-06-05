using DoctorWebApp.Models;
using DoctorWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebApp.Controllers
{


    public class DoctorController : Controller
    {
        public ActionResult Index()
        {
            return View(DoctorRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(DoctorRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DoctorRepository.Add(doctor);
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        public ActionResult Edit(int id)
        {
            return View(DoctorRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                DoctorRepository.Update(doctor);
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        public ActionResult Delete(int id)
        {
            return View(DoctorRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            DoctorRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}