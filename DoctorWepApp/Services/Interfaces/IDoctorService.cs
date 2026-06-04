using DoctorWepApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWepApp.Services.Interfaces
{
    public interface IDoctorService
    {
        bool AddDoctor(Doctor doctor);
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);
        void UpdateDoctor(Doctor doctor);
        void DeleteDoctor(int id);
    }
}