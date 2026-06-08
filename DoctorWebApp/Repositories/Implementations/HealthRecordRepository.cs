using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;

public class HealthRecordRepository : IHealthRecordRepository
{
    private static List<HealthRecord> _records = new List<HealthRecord>();
    private static int _id = 1;

    // ✅ Add record
    public void Add(HealthRecord record)
    {
        record.RecordId = _id++;
        _records.Add(record);
    }

    // ✅ Get all
    public IEnumerable<HealthRecord> GetAll()
    {
        return _records;
    }

    // ✅ Get by patient
    public IEnumerable<HealthRecord> GetByPatient(int patientId)
    {
        return _records
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.VisitDate);
    }

    // ✅ Get by doctor
    public IEnumerable<HealthRecord> GetByDoctor(int doctorId)
    {
        return _records
            .Where(r => r.DoctorId == doctorId);
    }

    // ✅ Prevent duplicate record
    public bool ExistsByAppointment(int appointmentId)
    {
        return _records
            .Any(r => r.AppointmentId == appointmentId);
    }
}
