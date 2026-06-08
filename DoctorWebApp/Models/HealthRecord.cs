using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebApp.Models
{
    public class HealthRecord
    {
        public int RecordId { get; set; }

        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public string DoctorSpecialisation { get; set; }

        public DateTime VisitDate { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        [Required]
        public string Prescription { get; set; }

        public string Notes { get; set; }
    }
}