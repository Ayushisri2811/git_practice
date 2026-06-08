using System.ComponentModel.DataAnnotations;

namespace DoctorWebApp.Models
{
    public enum SpecialisationType
    {
        GeneralPhysician,
        Cardiologist,
        Dermatologist,
        Neurologist,
        Orthopedic,
        Pediatrician,
        Psychiatrist,
        ENT,
        Gynecologist
    }

    public class Doctor
    {
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        public SpecialisationType Specialisation { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter valid 10-digit phone")]
        public string DoctorPhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string DoctorEmail { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        // ✅ Must be positive
        [Range(1, 10000, ErrorMessage = "Fee must be greater than 0")]
        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; } = true;

        // ✅ For profile page
        public int UpcomingAppointmentCount { get; set; }
    }
}