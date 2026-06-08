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
        // ✅ Send doctors to view (for dropdown)
        ViewBag.Doctors = _doctorRepo.GetAll(null, null);

        return View();
    }

    // ✅ BOOK APPOINTMENT (POST)
    [HttpPost]
    public ActionResult Create(Appointment model)
    {
        // ✅ Repopulate dropdown on postback (VERY IMPORTANT)
        ViewBag.Doctors = _doctorRepo.GetAll(null, null);

        // ✅ No past date
        if (model.ScheduledDate < DateTime.Today)
        {
            ModelState.AddModelError("", "Cannot select past date");
        }

        // ✅ Slot validation
        if (!_repo.IsSlotAvailable(model.DoctorId, model.ScheduledDate, model.TimeSlot))
        {
            ModelState.AddModelError("", "This time slot is already booked!");
        }

        if (ModelState.IsValid)
        {
            // ✅ Get doctor details
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
            patientId = 1; // default test
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
