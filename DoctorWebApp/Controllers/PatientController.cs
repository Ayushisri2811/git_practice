using DoctorWebApp.Models;
using DoctorWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebApp.Controllers
{


    public class PatientController : Controller
    {
        public ActionResult Index()
        {
            return View(PatientRepository.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                PatientRepository.Add(patient);
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public ActionResult Edit(int id)
        {
            return View(PatientRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                PatientRepository.Update(patient);
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public ActionResult Delete(int id)
        {
            return View(PatientRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            PatientRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(PatientRepository.GetById(id));
        }
    }
}