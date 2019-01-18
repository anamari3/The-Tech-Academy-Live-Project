        //Compute the property RequestLength.
        public TimeOffViewModel(TimeOffEvent timeOffEvent)
        {
                this.EventID = timeOffEvent.EventID;

                this.FirstName = timeOffEvent.User.FirstName;
                this.LastName = timeOffEvent.User.LastName;

                this.Start = timeOffEvent.Start;
                this.End = timeOffEvent.End;

                this.RequestLength = (End - Start);
  
                this.Note = timeOffEvent.Note;

                this.User = timeOffEvent.User;

                this.Submitted = timeOffEvent.Submitted;

                this.ActiveSchedule = timeOffEvent.ActiveSchedule ?? false; //if not set, imply property to be "false"
        }   
