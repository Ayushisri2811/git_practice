using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Repositories
{
    using DoctorWebApp.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class DoctorRepository
    {
        private static List<Doctor> doctors = new List<Doctor>
    {
        new Doctor
        {
            DoctorId = 1,
            FullName = "Dr. Rajesh Kumar",
            Specialisation = SpecialisationType.Cardiologist,
            DoctorPhoneNo = "9876543210",
            DoctorEmail = "rajesh@test.com",
            YearsOfExperience = 10,
            ConsultationFee = 500,
            IsActive = true
        }
    };

        public static List<Doctor> GetAll()
        {
            return doctors;
        }

        public static Doctor GetById(int id)
        {
            return doctors.FirstOrDefault(x => x.DoctorId == id);
        }

        public static void Add(Doctor doctor)
        {
            doctor.DoctorId = doctors.Count > 0
                ? doctors.Max(x => x.DoctorId) + 1
                : 1;

            doctors.Add(doctor);
        }

        public static void Update(Doctor doctor)
        {
            var existing = GetById(doctor.DoctorId);

            if (existing != null)
            {
                existing.FullName = doctor.FullName;
                existing.Specialisation = doctor.Specialisation;
                existing.DoctorPhoneNo = doctor.DoctorPhoneNo;
                existing.DoctorEmail = doctor.DoctorEmail;
                existing.YearsOfExperience = doctor.YearsOfExperience;
                existing.ConsultationFee = doctor.ConsultationFee;
                existing.IsActive = doctor.IsActive;
            }
        }

        public static void Delete(int id)
        {
            var doctor = GetById(id);

            if (doctor != null)
                doctors.Remove(doctor);
        }
    }
}