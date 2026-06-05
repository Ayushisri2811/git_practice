using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Repositories
{
    using DoctorWebApp.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class AppointmentRepository
    {
        private static List<Appointment> appointments =
            new List<Appointment>
            {
            new Appointment
            {
                AppointmentId = 1,
                PatientName = "John Smith",
                DoctorName = "Dr. Rajesh Kumar",
                ScheduledDate = DateTime.Today.AddDays(1),
                TimeSlot = "10:00 AM - 10:30 AM",
                Status = AppointmentStatus.Confirmed
            }
            };

        public static List<Appointment> GetAll()
        {
            return appointments;
        }

        public static Appointment GetById(int id)
        {
            return appointments.FirstOrDefault(x => x.AppointmentId == id);
        }

        public static void Add(Appointment appointment)
        {
            appointment.AppointmentId = appointments.Count > 0
                ? appointments.Max(x => x.AppointmentId) + 1
                : 1;

            appointments.Add(appointment);
        }

        public static void Update(Appointment appointment)
        {
            var existing = GetById(appointment.AppointmentId);

            if (existing != null)
            {
                existing.PatientName = appointment.PatientName;
                existing.DoctorName = appointment.DoctorName;
                existing.ScheduledDate = appointment.ScheduledDate;
                existing.TimeSlot = appointment.TimeSlot;
                existing.Status = appointment.Status;
                existing.CancellationReason = appointment.CancellationReason;
            }
        }

        public static void Delete(int id)
        {
            var appointment = GetById(id);

            if (appointment != null)
                appointments.Remove(appointment);
        }
    }
}