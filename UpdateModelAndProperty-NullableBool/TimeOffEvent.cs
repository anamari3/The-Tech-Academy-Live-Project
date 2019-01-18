namespace ScheduleUsers.Models
{
    /// <summary>
    /// Model for storing requests for time off. All events are stored in the Events Table
    /// </summary>
    public class TimeOffEvent:Event
    {

        [Key]
        /// <summary>
        /// ID for the event
        /// </summary>
        public Guid TimeOffEventID { get; set; }
        /// <summary>
        /// DateTime that displays date and time the time off event was requested.
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = "Submitted")]
        public DateTime? Submitted { get; set; }
        /// <summary>
        /// Whether or not the event is active on the schedule
        /// </summary>
        [Display(Name = "Approved")]
        public bool? ActiveSchedule { get; set; }
    }
}
