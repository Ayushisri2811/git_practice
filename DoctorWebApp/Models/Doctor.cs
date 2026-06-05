using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Models
{
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        public SpecialisationType Specialisation { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string DoctorPhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string DoctorEmail { get; set; }

        [Range(0, 50)]
        public int YearsOfExperience { get; set; }

        [Range(0, 10000)]
        public decimal ConsultationFee { get; set; }

        public bool IsActive { get; set; }
    }
}