using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HealthRecord
    {
        public int RecordId { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        public string Prescription { get; set; }

        public string Notes { get; set; }
    }
}