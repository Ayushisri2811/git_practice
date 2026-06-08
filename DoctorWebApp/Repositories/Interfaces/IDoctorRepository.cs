using DoctorWebApp.Models;
using System.Collections.Generic;

public interface IDoctorRepository
{
    IEnumerable<Doctor> GetAll(string specialisationFilter, string sortOrder);

    // ✅ NEW (recommended)
    IEnumerable<Doctor> GetActiveDoctors();

    Doctor GetById(int id);

    void Add(Doctor doctor);

    void Update(Doctor doctor);

    void ToggleStatus(int id);
}
