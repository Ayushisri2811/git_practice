using System.Linq;
using System.Web.Mvc;
using DoctorWebApp.Models;
using DoctorWebApp.Repository;

namespace DoctorWebApp.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorRepository _repo;

        public DoctorController()
        {
            _repo = new DoctorRepository();
        }

        // ✅ LIST (Search + Filter + Sort)
        public ActionResult Index(string specialisationFilter, string sortOrder, string searchTerm)
        {
            ViewBag.Specialisation = specialisationFilter;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchTerm = searchTerm;

            var doctors = _repo.GetAll(specialisationFilter, sortOrder);

            // ✅ SEARCH (ID + Name)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                doctors = doctors.Where(d =>
                    d.FullName.ToLower().Contains(searchTerm) ||
                    d.DoctorId.ToString().Contains(searchTerm)
                );
            }

            return View(doctors);
        }

        // ✅ CREATE (GET)
        public ActionResult Create()
        {
            return View();
        }

        // ✅ CREATE (POST)
        [HttpPost]
        public ActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            _repo.Add(doctor);
            return RedirectToAction("Index");
        }

        // ✅ DETAILS (PROFILE)
        public ActionResult Details(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor == null)
                return HttpNotFound();

            return View(doctor);
        }

        // ✅ EDIT (GET)
        public ActionResult Edit(int id)
        {
            var doctor = _repo.GetById(id);

            if (doctor == null)
                return HttpNotFound();

            return View(doctor);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (!ModelState.IsValid)
                return View(doctor);

            _repo.Update(doctor);
            return RedirectToAction("Index");
        }

        // ✅ TOGGLE ACTIVE / INACTIVE
        public ActionResult ToggleStatus(int id)
        {
            _repo.ToggleStatus(id);
            return RedirectToAction("Index");
        }
    }
}
