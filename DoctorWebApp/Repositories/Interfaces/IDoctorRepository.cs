using System.Collections.Generic;
using DoctorWebApp.Models;

namespace DoctorWebApp.Repository
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAll(string specialisationFilter, string sortOrder);
        Doctor GetById(int id);
        void Add(Doctor doctor);
        void Update(Doctor doctor);
        void ToggleStatus(int id);
    }
}