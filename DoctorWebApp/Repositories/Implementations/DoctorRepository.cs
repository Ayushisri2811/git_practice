using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;


namespace DoctorWebApp.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>()
        {
            // ✅ Default data (VERY IMPORTANT for dropdown)
            new Doctor
            {
                DoctorId = 1,
                FullName = "Dr. John Smith",
                Specialisation = SpecialisationType.Cardiologist,
                IsActive = true
            },
            new Doctor
            {
                DoctorId = 2,
                FullName = "Dr. Priya Nair",
                Specialisation = SpecialisationType.Dermatologist,
                IsActive = true
            },
            new Doctor
            {
                DoctorId = 3,
                FullName = "Dr. Rahul Kumar",
                Specialisation = SpecialisationType.Orthopedic,
                IsActive = true
            }
        };

        private static int _idCounter = 4;
        private static readonly object Specialisation;

        // ✅ Get all doctors with filter + sort
        public IEnumerable<Doctor> GetAll(string specialisationFilter, string sortOrder)
        {
            var doctors = _doctors.Where(d => d.IsActive).AsQueryable();

            // ✅ Filter
            if (!string.IsNullOrEmpty(specialisationFilter))
            {
                doctors = doctors.Where(d =>
                    d.Specialisation.ToString() == specialisationFilter);
            }

            // ✅ Sort
            doctors = sortOrder == "desc"
                ? doctors.OrderByDescending(d => d.FullName)
                : doctors.OrderBy(d => d.FullName);

            return doctors.ToList();
        }

        // ✅ NEW: Cleaner method for dropdown ✅
        public IEnumerable<Doctor> GetActiveDoctors()
        {
            return _doctors
                .Where(d => d.IsActive)
                .OrderBy(d => d.FullName)
                .ToList();
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

        // ✅ Toggle active/inactive status
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
