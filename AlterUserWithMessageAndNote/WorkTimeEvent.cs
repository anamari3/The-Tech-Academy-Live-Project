namespace ScheduleUsers.Models
{
    /// <summary>
    /// Model for storing when a user clocks in or out. All events are stored in the Events Table
    /// </summary>
    public class WorkTimeEvent:Event
    {
        public WorkTimeEvent()
        {
        }

        //Clock the user in by creating a new worktimeevent
        public WorkTimeEvent(ApplicationUser employee, string Note, DateTime? dt = null)
        {
            User = employee;          
            EventID = Guid.NewGuid();
            if (dt.HasValue) this.Start = dt.Value;
            else this.Start = DateTime.Now;
            this.Note = Note;
        }

        /// <summary>
        /// Used for clocking out and updating the time. If a datetime is passed,in  this is an update and returns 
        /// "ClockOutUpdated" on Success else it returns "ClockOutSuccess"
        /// </summary>
        /// <param name="dt"></param>
        /// <returns> This function returns a ClockFunctionStatus enum </returns>
        public ClockFunctionStatus Clockout(DateTime? dt = null)
        {
            try
            {
                //If we are clocking out but there is already a value, this is an update
                if (this.End.HasValue)
                {
                    this.End = dt.HasValue ? dt : DateTime.Now;
                    return ClockFunctionStatus.ClockOutUpdated;
                }
                else
                {
                    this.End = dt.HasValue ? dt : DateTime.Now;
                    return ClockFunctionStatus.ClockOutSuccess;
                }
            }
            catch
            {
                return ClockFunctionStatus.ClockOutFail;
            }
        }

        public enum ClockFunctionStatus
        {
            ClockInSuccess,
            ClockInFail,
            ClockOutSuccess,
            ClockOutFail,
            ClockInUpdated,
            ClockOutUpdated
        }

        public string ReadOnlyNote
        {
            get; set;
        }

        public bool? NeedsUpdating { get; set;}
    }
