       //overloaded method to include timeoffevents and worktime events on events calendar
        public static string EventstoFullCalendar<T>(List<T> workTimeEvents, List<T> timeOffEvents)
        {
            var list = new List<CalendarEvent>();

            foreach (var eachEventType in workTimeEvents)
            {
                var eachEvent = eachEventType as Event;

                list.Add(new CalendarEvent
                {
                    id = eachEvent.Id,
                    title = eachEvent.Title,
                    description = eachEvent.Note,
                    start = eachEvent.Start,
                    end = eachEvent.End,
                    color = "purple",
                    FirstName = eachEvent.User.FirstName,
                    LastName = eachEvent.User.LastName
                });

            }

            foreach (var eachEventType in timeOffEvents)
            {
                var eachEvent = eachEventType as TimeOffEvent;

                if (eachEvent.ActiveSchedule ?? false) //if not set, imply ActiveSchedule property to be "false"
                {
                    list.Add(new CalendarEvent
                    {
                        id = eachEvent.Id,
                        title = eachEvent.Title,
                        description = eachEvent.Note,
                        start = eachEvent.Start,
                        end = eachEvent.End,
                        color = "green",
                        FirstName = eachEvent.User.FirstName,
                        LastName = eachEvent.User.LastName
                    });
                }
                else
                {
                    list.Add(new CalendarEvent
                    {
                        id = eachEvent.Id,
                        title = eachEvent.Title,
                        description = eachEvent.Note,
                        start = eachEvent.Start,
                        end = eachEvent.End,
                        color = "red",
                        FirstName = eachEvent.User.FirstName,
                        LastName = eachEvent.User.LastName
                    });
                }


            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string eventstoFullCalendar = serializer.Serialize(list);

            return eventstoFullCalendar;
        }
