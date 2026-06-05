using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebApp.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        public AppointmentStatus Status { get; set; }

        public string CancellationReason { get; set; }
    }
}