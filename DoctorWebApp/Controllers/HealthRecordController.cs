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

    public class HealthRecordController : Controller
    {
        public ActionResult Index()
        {
            return View(HealthRecordRepository.GetAll());
        }

        public ActionResult Details(int id)
        {
            return View(HealthRecordRepository.GetById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HealthRecord record)
        {
            if (ModelState.IsValid)
            {
                HealthRecordRepository.Add(record);
                return RedirectToAction("Index");
            }

            return View(record);
        }

        public ActionResult Edit(int id)
        {
            return View(HealthRecordRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(HealthRecord record)
        {
            if (ModelState.IsValid)
            {
                HealthRecordRepository.Update(record);
                return RedirectToAction("Index");
            }

            return View(record);
        }

        public ActionResult Delete(int id)
        {
            return View(HealthRecordRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            HealthRecordRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}