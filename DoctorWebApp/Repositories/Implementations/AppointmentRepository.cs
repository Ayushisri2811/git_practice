using System;
using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;

public class AppointmentRepository : IAppointmentRepository
{
    private static List<Appointment> _appointments = new List<Appointment>();
    private static int _id = 1;

    // ✅ Get all appointments
    public IEnumerable<Appointment> GetAll()
    {
        return _appointments;
    }

    // ✅ Get appointments by patient
    public IEnumerable<Appointment> GetByPatient(int patientId)
    {
        return _appointments
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.ScheduledDate);
    }

    // ✅ Get today's schedule for doctor
    public IEnumerable<Appointment> GetByDoctorDate(int doctorId, DateTime date)
    {
        return _appointments
            .Where(a =>
                a.DoctorId == doctorId &&
                a.ScheduledDate.Date == date.Date)
            .OrderBy(a => a.TimeSlot);
    }

    // ✅ Get weekly schedule for doctor
    public IEnumerable<Appointment> GetByDoctorWeek(int doctorId, DateTime startDate)
    {
        var endDate = startDate.AddDays(7);

        return _appointments
            .Where(a =>
                a.DoctorId == doctorId &&
                a.ScheduledDate.Date >= startDate.Date &&
                a.ScheduledDate.Date <= endDate.Date)
            .OrderBy(a => a.ScheduledDate);
    }

    // ✅ Check slot availability (avoid double booking)
    public bool IsSlotAvailable(int doctorId, DateTime date, string slot)
    {
        return !_appointments.Any(a =>
            a.DoctorId == doctorId &&
            a.ScheduledDate.Date == date.Date &&
            a.TimeSlot == slot &&
            a.Status != AppointmentStatus.Cancelled);
    }

    // ✅ Add new appointment
    public void Add(Appointment appointment)
    {
        appointment.AppointmentId = _id++;
        appointment.Status = AppointmentStatus.Pending;
        appointment.HasHealthRecord = false;

        _appointments.Add(appointment);
    }

    // ✅ Update status (Confirm / Cancel / Complete)
    public void UpdateStatus(int id, AppointmentStatus status, string reason)
    {
        var app = _appointments.FirstOrDefault(a => a.AppointmentId == id);

        if (app != null)
        {
            app.Status = status;

            // ✅ Handle cancel reason
            if (status == AppointmentStatus.Cancelled)
            {
                app.CancellationReason = reason;
            }

            // ✅ Handle completed
            if (status == AppointmentStatus.Completed)
            {
                app.CompletedDate = DateTime.Now;
            }
        }
    }

    // ✅ Explicit mark complete method
    public void MarkCompleted(int appointmentId)
    {
        var app = _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

        if (app != null)
        {
            app.Status = AppointmentStatus.Completed;
            app.CompletedDate = DateTime.Now;
        }
    }

    // ✅ Check if health record already exists
    public bool HasHealthRecord(int appointmentId)
    {
        var app = _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

        return app != null && app.HasHealthRecord;
    }

    // ✅ Mark health record created
    public void SetHealthRecordCreated(int appointmentId)
    {
        var app = _appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

        if (app != null)
        {
            app.HasHealthRecord = true;
        }
    }
}
