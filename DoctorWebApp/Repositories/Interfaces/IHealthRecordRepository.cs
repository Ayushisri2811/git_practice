using System.Collections.Generic;
using DoctorWebApp.Models;

public interface IHealthRecordRepository
{
    // ✅ Add new health record
    void Add(HealthRecord record);

    // ✅ Get all records (optional, useful for admin)
    IEnumerable<HealthRecord> GetAll();

    // ✅ Get records by patient (for Patient Health History page)
    IEnumerable<HealthRecord> GetByPatient(int patientId);

    // ✅ Get records by doctor (optional, useful for doctor dashboard)
    IEnumerable<HealthRecord> GetByDoctor(int doctorId);

    // ✅ Check if record already exists for an appointment
    bool ExistsByAppointment(int appointmentId);
}

