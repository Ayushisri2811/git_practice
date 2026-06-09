using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;

public class HealthRecordRepository : IHealthRecordRepository
{
    private static List<HealthRecord> _records = new List<HealthRecord>();
    private static int _id = 1;

    // ✅ ADD RECORD
    public void Add(HealthRecord record)
    {
        record.RecordId = _id++;

        if (record.TreatmentDate == default)
        {
            record.TreatmentDate = System.DateTime.Now;
        }

        _records.Add(record);
    }

    // ✅ GET ALL
    public IEnumerable<HealthRecord> GetAll()
    {
        return _records;
    }

    // ✅ GET BY PATIENT
    public IEnumerable<HealthRecord> GetByPatient(int patientId)
    {
        return _records
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.AppointmentDate);
    }

    // ✅ GET BY DOCTOR
    public IEnumerable<HealthRecord> GetByDoctor(int doctorId)
    {
        return _records
            .Where(r => r.DoctorId == doctorId)
            .OrderByDescending(r => r.AppointmentDate);
    }

    // ✅ CHECK DUPLICATE
    public bool ExistsByAppointment(int appointmentId)
    {
        return _records.Any(r => r.AppointmentId == appointmentId);
    }

    // ✅ ✅ ✅ FIXED METHOD (THIS WAS MISSING)
    public HealthRecord GetByAppointment(int appointmentId)
    {
        return _records
            .FirstOrDefault(r => r.AppointmentId == appointmentId);
    }
}
