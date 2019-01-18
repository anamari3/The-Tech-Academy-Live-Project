        public static List<CalendarEvent> parseTimeOffEventsForCalendar(TimeOffEvent TimeOffRequest, List<CalendarEvent> ScheduledEvents)
        {
            if (TimeOffRequest.ActiveSchedule ?? false) //if not set, imply ActiveSchedule property to be "false"
            {
                //morningOff list finds events that coincide with a morning timeoff request, and updates each workPeriod's start time to reflect later start. wp = workPeriod
                List<CalendarEvent> morningOffWorkPeriods = ScheduledEvents.Where(wp => wp.start >= TimeOffRequest.Start).Where(x => x.start < TimeOffRequest.End).Where(wp => wp.end > TimeOffRequest.End).ToList();
                foreach (CalendarEvent morningOffWorkPeriod in morningOffWorkPeriods)
                {
                    morningOffWorkPeriod.start = TimeOffRequest.End;
                }

                //afternoonOff list finds events that coincide with an afternoon timeoff request, and updates each workPeriod's end time to reflect leaving early. wp = workPeriod
                List<CalendarEvent> afternoonOffWorkPeriods = ScheduledEvents.Where(wp => wp.start < TimeOffRequest.Start).Where(wp => wp.end > TimeOffRequest.Start).Where(wp => wp.end <= TimeOffRequest.End).ToList();
                foreach (CalendarEvent afternoonOffWorkPeriod in afternoonOffWorkPeriods)
                {
                    afternoonOffWorkPeriod.end = TimeOffRequest.Start;
                }

                //Create workperiod that PRECEEDS timeoff when timeoff is in the middle of the day
                List<CalendarEvent> part1WorkPeriods = ScheduledEvents.Where(wp => wp.start < TimeOffRequest.Start).Where(p => p.end > TimeOffRequest.End).ToList();
                foreach (CalendarEvent morningWorkPeriod in part1WorkPeriods)
                {
                    var workPeriodPart1 = morningWorkPeriod.Clone() as CalendarEvent;
                    workPeriodPart1.end = TimeOffRequest.Start;
                    ScheduledEvents.Add(workPeriodPart1);
                }

                //Create workperiod that FOLLOWS timeoff when timeoff is in the middle of the day
                List<CalendarEvent> part2WorkPeriods = ScheduledEvents.Where(wp => wp.start < TimeOffRequest.Start).Where(p => p.end > TimeOffRequest.End).ToList();
                foreach (CalendarEvent afternoonWorkPeriod in part2WorkPeriods)
                {
                    var workPeriodPart2 = afternoonWorkPeriod.Clone() as CalendarEvent;
                    workPeriodPart2.start = TimeOffRequest.End;
                    ScheduledEvents.Add(workPeriodPart2);
                }

                //removes workPeriods from original EventList where there exists a whole day off timeOffEvent
                ScheduledEvents.RemoveAll(wp => wp.start < TimeOffRequest.Start && wp.end > TimeOffRequest.End);

                //removes workPeriods from original EventList where there exists a whole day off timeOffEvent
                ScheduledEvents.RemoveAll(wp => wp.start >= TimeOffRequest.Start && wp.end <= TimeOffRequest.End);

                //adds approved time off request to schedule view
                ScheduledEvents.Add(new CalendarEvent
                {
                    id = TimeOffRequest.Id,
                    title = TimeOffRequest.Title,
                    description = TimeOffRequest.Note,
                    start = TimeOffRequest.Start,
                    end = TimeOffRequest.End,
                    color = "green",
                    FirstName = TimeOffRequest.User.FirstName,
                    LastName = TimeOffRequest.User.LastName
                });
            }
            else
            {
                ScheduledEvents.Add(new CalendarEvent
                    {
                        id = TimeOffRequest.Id,
                        title = TimeOffRequest.Title,
                        description = TimeOffRequest.Note,
                        start = TimeOffRequest.Start,
                        end = TimeOffRequest.End,
                        color = "red",
                        FirstName = TimeOffRequest.User.FirstName,
                        LastName = TimeOffRequest.User.LastName
                    }
                );
            }

            return ScheduledEvents;
        }
