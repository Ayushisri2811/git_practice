using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebApp.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Appointment : IValidatableObject   // ✅ Added interface
    {
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Patient is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        public int DoctorId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public string DoctorSpecialisation { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Time slot is required")]
        public string TimeSlot { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public string CancellationReason { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool HasHealthRecord { get; set; } = false;

        // ✅ ✅ CUSTOM VALIDATION
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(TimeSlot))
            {
                // Use TryParse so we don't need a try/catch with yield inside it
                if (!DateTime.TryParse(TimeSlot, out DateTime time))
                {
                    yield return new ValidationResult(
                        "Invalid time slot format.",
                        new[] { nameof(TimeSlot) }
                    );
                    yield break;
                }

                // Combine date + time
                DateTime appointmentDateTime = ScheduledDate.Date.Add(time.TimeOfDay);

                if (appointmentDateTime <= DateTime.Now)
                {
                    yield return new ValidationResult(
                        "Appointment date and time must be in the future.",
                        new[] { nameof(ScheduledDate), nameof(TimeSlot) }
                    );
                }
            }
        }
    }
}
