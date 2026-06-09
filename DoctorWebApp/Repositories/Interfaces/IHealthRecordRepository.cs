using System.Collections.Generic;
using DoctorWebApp.Models;

public interface IHealthRecordRepository
{
    // ✅ Add new health record
    void Add(HealthRecord record);

    // ✅ Get all records (admin / debugging)
    IEnumerable<HealthRecord> GetAll();

    // ✅ Get records by patient (Patient History page)
    IEnumerable<HealthRecord> GetByPatient(int patientId);

    // ✅ Get records by doctor (Doctor dashboard)
    IEnumerable<HealthRecord> GetByDoctor(int doctorId);

    // ✅ Check if record already exists for an appointment
    bool ExistsByAppointment(int appointmentId);

    // ✅ NEW: Get record by appointment (useful for direct lookup)
    HealthRecord GetByAppointment(int appointmentId);
}

