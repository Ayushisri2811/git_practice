using HealthAppMVC.Services.Interface;
using SharedDto.PatientDtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientApiService _patientService;

        public PatientController(IPatientApiService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public async Task<ActionResult> Index()
        {
            var patients =
                await _patientService.GetAllPatientsAsync();

            return View(patients);
        }

        // GET: Patient/Details/5
        // Normal page fallback
        public async Task<ActionResult> Details(int id)
        {
            var patient =
                await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            return View(patient);
        }

        // GET: Patient/DetailsPartial/5
        // Used for View popup modal
        public async Task<ActionResult> DetailsPartial(int id)
        {
            var patient =
                await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PatientDetailsModal", patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePatientDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                await _patientService.CreatePatientAsync(dto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(dto);
            }
        }

        // GET: Patient/Edit/5
        // Normal page fallback
        public async Task<ActionResult> Edit(int id)
        {
            var patient =
                await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            CreatePatientDto dto =
                new CreatePatientDto
                {
                    FullName = patient.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    InsuranceId = patient.InsuranceId
                };

            ViewBag.PatientId = id;

            return View(dto);
        }

        // GET: Patient/EditPartial/5
        // Used for Edit popup modal
        public async Task<ActionResult> EditPartial(int id)
        {
            var patient =
                await _patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return HttpNotFound();
            }

            CreatePatientDto dto =
                new CreatePatientDto
                {
                    FullName = patient.FullName,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,
                    InsuranceId = patient.InsuranceId
                };

            ViewBag.PatientId = id;

            return PartialView("_PatientEditModal", dto);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreatePatientDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.PatientId = id;

                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("_PatientEditModal", dto);
                    }

                    return View(dto);
                }

                await _patientService.UpdatePatientAsync(id, dto);

                if (Request.IsAjaxRequest())
                {
                    return Json(new
                    {
                        success = true,
                        message = "Patient updated successfully."
                    });
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.PatientId = id;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_PatientEditModal", dto);
                }

                return View(dto);
            }
        }

        // GET: Patient/SearchPatient
        // Search page for editing patient
        public async Task<ActionResult> SearchPatient()
        {
            var patients =
                await _patientService.GetAllPatientsAsync();

            return View(patients);
        }

        // GET: Patient/SearchPatientNames?term=abc
        // Used for autocomplete if needed
        public async Task<JsonResult> SearchPatientNames(string term)
        {
            var patients =
                await _patientService.SearchByNameAsync(term);

            var result =
                patients.Select(p => new
                {
                    id = p.PatientId,
                    label = p.FullName,
                    value = p.FullName
                }).ToList();

            return Json(
                result,
                JsonRequestBehavior.AllowGet);
        }

        // GET: Patient/EditPatientByName/5
        public ActionResult EditPatientByName(int id)
        {
            return RedirectToAction(
                "Edit",
                new { id = id });
        }

        // GET: Patient/PatientSearch
        // View patient details search page
        public async Task<ActionResult> PatientSearch(string patientName)
        {
            var patients =
                await _patientService.GetAllPatientsAsync();

            if (!string.IsNullOrWhiteSpace(patientName))
            {
                string searchValue =
                    patientName.ToLower();

                patients =
                    patients
                    .Where(p =>
                        p.FullName.ToLower().Contains(searchValue)
                        ||
                        p.PatientId.ToString().Contains(searchValue))
                    .ToList();
            }

            return View(patients);
        }
    }
}