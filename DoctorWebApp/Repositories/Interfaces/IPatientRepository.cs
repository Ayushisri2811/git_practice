using System.Collections.Generic;
using DoctorWebApp.Models;

namespace DoctorWebApp.Repository
{
    public interface IPatientRepository
    {
        // ✅ Updated: Added searchTerm
        IEnumerable<Patient> GetAll(string sortOrder, string insuranceFilter, string searchTerm);

        Patient GetById(int id);

        void Add(Patient patient);

        // ✅ Returns bool to handle duplicate email on update
        bool Update(Patient patient);

        void Deactivate(int id);

        bool EmailExists(string email);
    }
}

