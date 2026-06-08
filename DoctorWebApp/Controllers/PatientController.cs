using System.Web.Mvc;
using DoctorWebApp.Models;
using DoctorWebApp.Repository;

namespace DoctorWebApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _repo;

        public PatientController()
        {
            _repo = new PatientRepository();
        }

        // ✅ INDEX (Search + Filter + Sort)
        public ActionResult Index(string sortOrder, string insuranceFilter, string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortOrder = sortOrder;
            ViewBag.InsuranceFilter = insuranceFilter;

            var patients = _repo.GetAll(sortOrder, insuranceFilter, searchTerm);
            return View(patients);
        }

        // ✅ CREATE
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (_repo.EmailExists(patient.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
            }

            if (ModelState.IsValid)
            {
                _repo.Add(patient);
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // ✅ DETAILS
        public ActionResult Details(int id)
        {
            var patient = _repo.GetById(id);
            if (patient == null) return HttpNotFound();

            return View(patient);
        }

        // ✅ EDIT
        public ActionResult Edit(int id)
        {
            var patient = _repo.GetById(id);
            if (patient == null) return HttpNotFound();

            return View(patient);
        }

        [HttpPost]
        public ActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            if (!_repo.Update(patient))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(patient);
            }

            return RedirectToAction("Index");
        }

        // ✅ DEACTIVATE
        public ActionResult Deactivate(int id)
        {
            var patient = _repo.GetById(id);
            if (patient == null) return HttpNotFound();

            return View(patient);
        }

        [HttpPost, ActionName("Deactivate")]
        public ActionResult DeactivateConfirmed(int id)
        {
            _repo.Deactivate(id);
            return RedirectToAction("Index");
        }
    }
}
