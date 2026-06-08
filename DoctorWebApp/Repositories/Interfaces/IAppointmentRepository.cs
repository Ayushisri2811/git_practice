using System;
using System.Collections.Generic;
using DoctorWebApp.Models;

public interface IAppointmentRepository
{
    // ✅ Basic retrieval
    IEnumerable<Appointment> GetAll();

    IEnumerable<Appointment> GetByPatient(int patientId);

    IEnumerable<Appointment> GetByDoctorDate(int doctorId, DateTime date);

    IEnumerable<Appointment> GetByDoctorWeek(int doctorId, DateTime startDate);

    // ✅ Slot validation
    bool IsSlotAvailable(int doctorId, DateTime date, string slot);

    // ✅ CRUD operations
    void Add(Appointment appointment);

    // ✅ Status update (Confirm / Cancel / Complete)
    void UpdateStatus(int id, AppointmentStatus status, string reason);

    // ✅ NEW: Mark appointment as completed explicitly
    void MarkCompleted(int appointmentId);

    // ✅ NEW: Check if health record exists
    bool HasHealthRecord(int appointmentId);

    // ✅ NEW: Mark health record created
    void SetHealthRecordCreated(int appointmentId);
}
