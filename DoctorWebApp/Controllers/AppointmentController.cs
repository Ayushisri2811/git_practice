using System.Web.Mvc;
using DoctorWebApp.Models;
using DoctorWebApp.Repositories;

namespace DoctorWebApp.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View(AppointmentRepository.GetAll());
        }

        // GET: Details
        public ActionResult Details(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            ModelState.Remove("AppointmentId"); // IMPORTANT FIX

            if (ModelState.IsValid)
            {
                AppointmentRepository.Add(appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        // POST: Edit
        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            ModelState.Remove("AppointmentId");

            if (ModelState.IsValid)
            {
                AppointmentRepository.Update(appointment);
                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        // GET: Delete
        public ActionResult Delete(int id)
        {
            return View(AppointmentRepository.GetById(id));
        }

        // POST: Delete
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            AppointmentRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}