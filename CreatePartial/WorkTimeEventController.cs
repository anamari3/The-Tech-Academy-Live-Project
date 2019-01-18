    public ActionResult ViewWorkTimeEventDetails(DateTime startDate, DateTime endDate, DateTime payPeriod, DateTime payYear)
    {
 
                EventListVm eventDetails = new EventListVm();

                return PartialView("_ViewWorkTimeEventDetails", eventDetails);
    }

        public ActionResult WorkTimePartial(DateTime? start, DateTime? end) //these parameters come from the getWTEPartial() function on the Index.cshtml page
        {
            if (!start.HasValue)
            {
                start = DateTime.Now.AddDays(-14);
            }
            if (!end.HasValue)
            {
                end = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            // Grabs the current user ID
            var userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            bool ClockedInStatus = db.WorkTimeEvents.Any(x => x.Id == userId && !x.End.HasValue);
            //Sorting the order to pre-populate the table with dates in ascending oerder
            DateTime? DisplayBeginDate = start;
            DateTime? DisplayEndDate = end;
            //This variable is taking in selected dates from the user and and ensuring that the dates are filtered correctly and sorted.
            var Sorting = db.WorkTimeEvents.Where(w => w.User.Id == userId).Where(w =>
                                                              w.End <= DisplayEndDate &&
                                                              w.End >= DisplayBeginDate ||
                                                              w.Start <= DisplayEndDate &&
                                                              w.Start >= DisplayBeginDate ||
                                                              w.Start <= DisplayBeginDate &&
                                                              w.End >= DisplayEndDate
                                                              ).OrderBy(w => w.Start);
            var payPeriod = db.PayPeriods.FirstOrDefault();
            var year = payPeriod.StartDate.Year;
            EventListVm EVM = new EventListVm(year, workTime: Sorting.ToList(), user: user, ClockedIn: ClockedInStatus);
            return PartialView(EVM);
        }

