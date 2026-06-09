using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebApp.Models
{
    public class HealthRecord
    {
        public int RecordId { get; set; }

        // ✅ Mapping IDs
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        // ✅ Display fields
        public string PatientName { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecialisation { get; set; }

        // ✅ FIXED: Appointment date (from Appointment table)
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        // ✅ NEW: When treatment actually recorded
        [DataType(DataType.Date)]
        public DateTime TreatmentDate { get; set; } = DateTime.Now;

        // ✅ Medical details
        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Prescription is required")]
        public string Prescription { get; set; }

        public string Notes { get; set; }
    }
}