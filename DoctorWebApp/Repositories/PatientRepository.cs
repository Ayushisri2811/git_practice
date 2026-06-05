using DoctorWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DoctorWebApp.Repositories
{

    public static class PatientRepository
    {
        private static List<Patient> patients = new List<Patient>
    {
        new Patient
        {
            PatientId = 1,
            FullName = "John Smith",
            DateOfBirth = new DateTime(1990,5,10),
            Gender = GenderType.Male,
            PhoneNumber = "9876543210",
            Email = "john@test.com",
            InsuranceId = "INS001"
        }
    };

        public static List<Patient> GetAll()
        {
            return patients;
        }

        public static Patient GetById(int id)
        {
            return patients.FirstOrDefault(x => x.PatientId == id);
        }

        public static void Add(Patient patient)
        {
            patient.PatientId = patients.Count > 0
                ? patients.Max(x => x.PatientId) + 1
                : 1;

            patients.Add(patient);
        }

        public static void Update(Patient patient)
        {
            var existing = GetById(patient.PatientId);

            if (existing != null)
            {
                existing.FullName = patient.FullName;
                existing.DateOfBirth = patient.DateOfBirth;
                existing.Gender = patient.Gender;
                existing.PhoneNumber = patient.PhoneNumber;
                existing.Email = patient.Email;
                existing.InsuranceId = patient.InsuranceId;
            }
        }

        public static void Delete(int id)
        {
            var patient = GetById(id);

            if (patient != null)
                patients.Remove(patient);
        }
    }
}