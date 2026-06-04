using DoctorWepApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWepApp.Repositories.Interfaces
{

    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        List<Doctor> GetAll();
        Doctor GetById(int id);
        void Update(Doctor doctor);
        void Delete(int id);
    }
}
