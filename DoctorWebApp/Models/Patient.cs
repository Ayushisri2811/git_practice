using System;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public GenderType Gender { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid 10-digit phone")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public string InsuranceId { get; set; }

        public bool IsActive { get; set; } = true;

        public int AppointmentCount { get; set; } // for profile view

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}