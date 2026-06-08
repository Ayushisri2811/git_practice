using System.Collections.Generic;
using System.Linq;
using DoctorWebApp.Models;

namespace DoctorWebApp.Repository
{
    public class PatientRepository : IPatientRepository
    {
        // ✅ In-memory storage
        private static List<Patient> _patients = new List<Patient>();
        private static int _idCounter = 1;

        // ✅ Get all patients (Search + Filter + Sort)
        public IEnumerable<Patient> GetAll(string sortOrder, string insuranceFilter, string searchTerm)
        {
            var patients = _patients
                .Where(p => p.IsActive) // ✅ Only active patients
                .AsQueryable();

            // ✅ SEARCH (by ID or Name)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower().Trim();

                patients = patients.Where(p =>
                    p.FullName.ToLower().Contains(searchTerm) ||
                    p.PatientId.ToString().Contains(searchTerm)
                );
            }

            // ✅ FILTER (Insurance)
            if (!string.IsNullOrEmpty(insuranceFilter))
            {
                if (insuranceFilter == "WithInsurance")
                    patients = patients.Where(p => !string.IsNullOrWhiteSpace(p.InsuranceId));

                else if (insuranceFilter == "WithoutInsurance")
                    patients = patients.Where(p => string.IsNullOrWhiteSpace(p.InsuranceId));
            }

            // ✅ SORT (Name)
            patients = sortOrder == "desc"
                ? patients.OrderByDescending(p => p.FullName)
                : patients.OrderBy(p => p.FullName);

            return patients.ToList();
        }

        // ✅ Get by Id
        public Patient GetById(int id)
        {
            return _patients.FirstOrDefault(p => p.PatientId == id);
        }

        // ✅ Add Patient
        public void Add(Patient patient)
        {
            if (patient == null) return;

            patient.PatientId = _idCounter++;
            patient.IsActive = true;

            _patients.Add(patient);
        }

        // ✅ Update Patient
        public bool Update(Patient patient)
        {
            var existing = GetById(patient.PatientId);
            if (existing == null) return false;

            // ✅ Prevent duplicate email (excluding self)
            if (_patients.Any(p =>
                p.PatientId != patient.PatientId &&
                p.Email.ToLower().Trim() == patient.Email.ToLower().Trim()))
            {
                return false;
            }

            existing.FullName = patient.FullName;
            existing.DateOfBirth = patient.DateOfBirth;
            existing.Gender = patient.Gender;
            existing.PhoneNumber = patient.PhoneNumber;
            existing.Email = patient.Email;
            existing.InsuranceId = patient.InsuranceId;

            return true;
        }

        // ✅ Soft delete
        public void Deactivate(int id)
        {
            var patient = GetById(id);
            if (patient != null)
            {
                patient.IsActive = false;
            }
        }

        // ✅ Check duplicate email
        public bool EmailExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            return _patients.Any(p =>
                p.Email.ToLower().Trim() == email.ToLower().Trim());
        }
    }
}
