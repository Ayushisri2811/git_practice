using HealthAppMVC.Services.Interface;
using SharedDto.HealthRecordDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HealthAppMVC.Controllers
{
    public class HealthRecordController : Controller
    {
        private readonly IHealthRecordApiService _healthRecordService;
        private readonly IPatientApiService _patientService;

        public HealthRecordController(
            IHealthRecordApiService healthRecordService,
            IPatientApiService patientService)
        {
            _healthRecordService = healthRecordService;
            _patientService = patientService;
        }

        // ✅ ✅ NEW: LOAD HISTORY POPUP
        public ActionResult PatientHistory()
        {
            return PartialView("_PatientHistory");
        }

        // ✅ ✅ UPDATED: CREATE (GET) → POPUP
        [HttpGet]
        public ActionResult Create(int appointmentId = 0)
        {
            var dto = new CreateHealthRecordDto
            {
                AppointmentId = appointmentId
            };

            // 🔥 IMPORTANT: return PartialView instead of View
            return PartialView("_AddHealthRecord", dto);
        }

        // ✅ ✅ UPDATED: CREATE (POST) → AJAX SUPPORT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateHealthRecordDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_AddHealthRecord", dto);
                }

                await _healthRecordService.AddAsync(dto);

                // ✅ For AJAX success
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_AddHealthRecord", dto);
            }
        }

        // ✅ DETAILS (no change)
        public async Task<ActionResult> Details(int id)
        {
            var record = await _healthRecordService.GetByIdAsync(id);

            if (record == null)
                return HttpNotFound();

            return View(record);
        }

        // ✅ ✅ KEEP THIS (for normal page navigation if needed)
        public async Task<ActionResult> SearchPatientHistory(int? patientId)
        {
            IEnumerable<HealthRecordDto> records =
                Enumerable.Empty<HealthRecordDto>();

            if (patientId.HasValue)
            {
                records = await _healthRecordService
                    .GetPatientHistoryAsync(patientId.Value);
            }

            return View(records);
        }

        // ✅ ✅ NEW: AJAX HISTORY (IMPORTANT)
        public async Task<JsonResult> SearchPatientHistoryAjax(int patientId)
        {
            var records = await _healthRecordService
                .GetPatientHistoryAsync(patientId);

            var result = records.Select(x => new
            {
                VisitDateFormatted = x.VisitDate.ToString("dd-MM-yyyy"),
                x.DoctorName,
                x.Diagnosis,
                x.Prescription,
                x.Notes
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // ✅ AUTOCOMPLETE (no change)
        public async Task<JsonResult> SearchPatientNames(string term)
        {
            var patients = await _patientService
                .SearchByNameAsync(term);

            var result = patients.Select(p => new
            {
                label = p.FullName,
                value = p.PatientId
            });

            return Json(result.ToList(),
                        JsonRequestBehavior.AllowGet);
        }
    }
}
