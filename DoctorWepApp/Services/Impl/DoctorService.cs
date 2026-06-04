using DoctorWepApp.Models;
using DoctorWepApp.Repositories.Interfaces;
using DoctorWepApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWepApp.Services.Impl
{


    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public bool AddDoctor(Doctor doctor)
        {
            // ✅ Business Logic
            if (!doctor.IsValidEmail() ||
                !doctor.IsValidPhoneNumber() ||
                !doctor.IsValidName())
            {
                return false;
            }

            doctor.IsActive = true;
            _repo.Add(doctor);
            return true;
        }

        public List<Doctor> GetAllDoctors()
        {
            return _repo.GetAll();
        }

        public Doctor GetDoctorById(int id)
        {
            return _repo.GetById(id);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            _repo.Update(doctor);
        }

        public void DeleteDoctor(int id)
        {
            _repo.Delete(id);
        }
    }
}

