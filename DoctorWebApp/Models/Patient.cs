using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace DoctorWebApp.Models
{

    public enum GenderType
    {
        Male,
        Female,
        Other
    }

    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string InsuranceId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}