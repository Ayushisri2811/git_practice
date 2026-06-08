using System;
using System.Linq;
using System.Web.Mvc;
using DoctorWebApp.Models;

namespace DoctorWebApp.Controllers
{
    public class HealthRecordController : Controller
    {
        private IHealthRecordRepository _repo = new HealthRecordRepository();
        private IAppointmentRepository _appRepo = new AppointmentRepository();

        // ✅ Handles /HealthRecord or /HealthRecord/Index
        // No Index view needed ✅
        public ActionResult Index()
        {
            return RedirectToAction("PatientHistory", new { patientId = 1 });
        }

        // ✅ ADD RECORD (GET)
        public ActionResult AddRecord(int appointmentId)
        {
            var appointment = _appRepo.GetAll()
                                      .FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment == null)
                return HttpNotFound();

            // ✅ Prevent duplicate record
            if (_repo.ExistsByAppointment(appointmentId))
            {
                return RedirectToAction("PatientHistory",
                    new { patientId = appointment.PatientId });
            }

            // ✅ Prepare model
            var model = new HealthRecord
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                PatientName = appointment.PatientName,
                DoctorName = appointment.DoctorName,
                DoctorSpecialisation = appointment.DoctorSpecialisation,
                VisitDate = DateTime.Now
            };

            return View(model);
        }

        // ✅ ADD RECORD (POST)
        [HttpPost]
        public ActionResult AddRecord(HealthRecord model)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(model);

                // ✅ mark appointment as having record
                _appRepo.SetHealthRecordCreated(model.AppointmentId);

                return RedirectToAction("PatientHistory",
                    new { patientId = model.PatientId });
            }

            return View(model);
        }

        // ✅ PATIENT HISTORY
        public ActionResult PatientHistory(int? patientId)
        {
            if (patientId == null)
                patientId = 1;   // default for testing

            var data = _repo.GetByPatient(patientId.Value);

            return View(data);
        }
    }
}
