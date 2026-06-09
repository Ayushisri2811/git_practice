using System;
using System.Linq;
using System.Web.Mvc;
using DoctorWebApp.Models;
using DoctorWebApp.Repository;

public class AppointmentController : Controller
{
    private IAppointmentRepository _repo = new AppointmentRepository();
    private IDoctorRepository _doctorRepo = new DoctorRepository();

    // ✅ DASHBOARD
    public ActionResult Index()
    {
        return View();
    }

    // ✅ BOOK APPOINTMENT (GET)
    public ActionResult Create()
    {
        ViewBag.Doctors = _doctorRepo.GetAll(null, null);
        return View();
    }

    // ✅ BOOK APPOINTMENT (POST)
    [HttpPost]
    public ActionResult Create(Appointment model)
    {
        // ✅ Repopulate dropdown
        ViewBag.Doctors = _doctorRepo.GetAll(null, null);

        // ✅ ✅ FIXED: Validate past date + time
        if (!string.IsNullOrEmpty(model.TimeSlot))
        {
            try
            {
                DateTime parsedTime = DateTime.Parse(model.TimeSlot);

                DateTime appointmentDateTime = model.ScheduledDate.Date.Add(parsedTime.TimeOfDay);

                if (appointmentDateTime <= DateTime.Now)
                {
                    ModelState.AddModelError("", "Cannot book appointment in the past date and time.");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Invalid time slot format.");
            }
        }

        // ✅ ✅ Slot validation
        if (!_repo.IsSlotAvailable(model.DoctorId, model.ScheduledDate, model.TimeSlot))
        {
            ModelState.AddModelError("", "This time slot is already booked!");
        }

        // ✅ ✅ Final save
        if (ModelState.IsValid)
        {
            var doctor = _doctorRepo
                .GetAll(null, null)
                .FirstOrDefault(d => d.DoctorId == model.DoctorId);

            if (doctor != null)
            {
                model.DoctorName = doctor.FullName;
                model.DoctorSpecialisation = doctor.Specialisation.ToString();
            }

            model.PatientName = "Patient " + model.PatientId;

            _repo.Add(model);

            return RedirectToAction("MyAppointments", new { patientId = model.PatientId });
        }

        return View(model);
    }

    // ✅ PATIENT VIEW
    public ActionResult MyAppointments(int? patientId)
    {
        if (patientId == null)
        {
            patientId = 1;
        }

        var data = _repo.GetByPatient(patientId.Value);
        return View(data);
    }

    // ✅ DOCTOR TODAY
    public ActionResult TodaySchedule(int? doctorId)
    {
        if (doctorId == null)
        {
            doctorId = 1;
        }

        var data = _repo.GetByDoctorDate(doctorId.Value, DateTime.Today);
        return View(data);
    }

    // ✅ DOCTOR WEEKLY
    public ActionResult WeeklySchedule(int? doctorId)
    {
        if (doctorId == null)
        {
            doctorId = 1;
        }

        var data = _repo.GetByDoctorWeek(doctorId.Value, DateTime.Today);
        return View(data);
    }

    // ✅ CONFIRM / CANCEL
    public ActionResult UpdateStatus(int id, AppointmentStatus status, string reason, int doctorId)
    {
        _repo.UpdateStatus(id, status, reason);
        return RedirectToAction("TodaySchedule", new { doctorId = doctorId });
    }

    // ✅ COMPLETE APPOINTMENT
    public ActionResult MarkCompleted(int id, int doctorId)
    {
        _repo.MarkCompleted(id);
        return RedirectToAction("AddRecord", "HealthRecord", new { appointmentId = id });
    }
}