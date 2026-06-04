using DoctorWepApp.Models;
using DoctorWepApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWepApp.Repositories.Impl
{

    public class DoctorRepository : IDoctorRepository
    {
        private static List<Doctor> doctors = new List<Doctor>();

        public void Add(Doctor doctor)
        {
            doctor.DoctorId = doctors.Count + 1;
            doctors.Add(doctor);
        }

        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetById(int id)
        {
            return doctors.FirstOrDefault(d => d.DoctorId == id);
        }

        public void Update(Doctor updatedDoctor)
        {
            var doctor = doctors.FirstOrDefault(d => d.DoctorId == updatedDoctor.DoctorId);
            if (doctor != null)
            {
                doctor.FullName = updatedDoctor.FullName;
                doctor.DoctorEmail = updatedDoctor.DoctorEmail;
                doctor.DoctorPhoneNo = updatedDoctor.DoctorPhoneNo;
                doctor.Specialisation = updatedDoctor.Specialisation;
                doctor.YearsOfExperience = updatedDoctor.YearsOfExperience;
                doctor.ConsultationFee = updatedDoctor.ConsultationFee;
            }
        }

        public void Delete(int id)
        {
            var doctor = doctors.FirstOrDefault(d => d.DoctorId == id);
            if (doctor != null)
                doctors.Remove(doctor);
        }
    }
}
