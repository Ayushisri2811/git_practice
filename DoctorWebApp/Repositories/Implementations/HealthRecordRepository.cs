using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Repositories
{
    using DoctorWebApp.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class HealthRecordRepository
    {
        private static List<HealthRecord> records =
            new List<HealthRecord>
            {
            new HealthRecord
            {
                RecordId = 1,
                PatientName = "John Smith",
                DoctorName = "Dr. Rajesh Kumar",
                VisitDate = DateTime.Today,
                Diagnosis = "Fever",
                Prescription = "Paracetamol",
                Notes = "Take rest for 3 days"
            }
            };

        public static List<HealthRecord> GetAll()
        {
            return records;
        }

        public static HealthRecord GetById(int id)
        {
            return records.FirstOrDefault(x => x.RecordId == id);
        }

        public static void Add(HealthRecord record)
        {
            record.RecordId = records.Count > 0
                ? records.Max(x => x.RecordId) + 1
                : 1;

            records.Add(record);
        }

        public static void Update(HealthRecord record)
        {
            var existing = GetById(record.RecordId);

            if (existing != null)
            {
                existing.PatientName = record.PatientName;
                existing.DoctorName = record.DoctorName;
                existing.VisitDate = record.VisitDate;
                existing.Diagnosis = record.Diagnosis;
                existing.Prescription = record.Prescription;
                existing.Notes = record.Notes;
            }
        }

        public static void Delete(int id)
        {
            var record = GetById(id);

            if (record != null)
                records.Remove(record);
        }
    }
}