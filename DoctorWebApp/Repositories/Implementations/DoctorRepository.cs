using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;

namespace DoctorWebApp.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>();
        private static int _idCounter = 1;

        public IEnumerable<Doctor> GetAll(string specialisationFilter, string sortOrder)
        {
            var doctors = _doctors.Where(d => d.IsActive).AsQueryable();

            // ✅ Filter
            if (!string.IsNullOrEmpty(specialisationFilter))
            {
                doctors = doctors.Where(d => d.Specialisation.ToString() == specialisationFilter);
            }

            // ✅ Sort
            doctors = sortOrder == "desc"
                ? doctors.OrderByDescending(d => d.FullName)
                : doctors.OrderBy(d => d.FullName);

            return doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return _doctors.FirstOrDefault(d => d.DoctorId == id);
        }

        public void Add(Doctor doctor)
        {
            doctor.DoctorId = _idCounter++;
            doctor.IsActive = true;
            _doctors.Add(doctor);
        }

        public void Update(Doctor doctor)
        {
            var existing = GetById(doctor.DoctorId);
            if (existing == null) return;

            existing.FullName = doctor.FullName;
            existing.Specialisation = doctor.Specialisation;
            existing.DoctorPhoneNo = doctor.DoctorPhoneNo;
            existing.DoctorEmail = doctor.DoctorEmail;
            existing.YearsOfExperience = doctor.YearsOfExperience;
            existing.ConsultationFee = doctor.ConsultationFee;
        }

        // ✅ Toggle Active/Inactive
        public void ToggleStatus(int id)
        {
            var doctor = GetById(id);
            if (doctor != null)
            {
                doctor.IsActive = !doctor.IsActive;
            }
        }
    }
}