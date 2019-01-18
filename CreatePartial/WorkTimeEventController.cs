    public ActionResult ViewWorkTimeEventDetails(DateTime startDate, DateTime endDate, DateTime payPeriod, DateTime payYear)
    {
 
                EventListVm eventDetails = new EventListVm();

                return PartialView("_ViewWorkTimeEventDetails", eventDetails);
     }
