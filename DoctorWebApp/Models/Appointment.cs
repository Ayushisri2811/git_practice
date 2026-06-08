using System;
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

    public class Appointment
    {
        public int AppointmentId { get; set; }

        // ✅ Patient & Doctor mapping
        [Required(ErrorMessage = "Patient is required")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor is required")]
        public int DoctorId { get; set; }

        // ✅ Display fields (important for UI)
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        // ✅ Added for Health History requirement
        public string DoctorSpecialisation { get; set; }

        // ✅ Appointment date
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        // ✅ Slot selection
        [Required(ErrorMessage = "Time slot is required")]
        public string TimeSlot { get; set; }

        // ✅ Status lifecycle
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        // ✅ Cancel reason
        public string CancellationReason { get; set; }

        // ✅ NEW: Track completion date (used for health records)
        public DateTime? CompletedDate { get; set; }

        // ✅ NEW: Flag for checking if health record exists
        public bool HasHealthRecord { get; set; } = false;
    }
}
