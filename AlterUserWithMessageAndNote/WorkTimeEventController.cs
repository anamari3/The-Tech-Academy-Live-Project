        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        [ActionName("Edit")]
        public ActionResult EditTime(Guid? Id) //gets the worktimeEvent ID
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var EventToUpdate = db.WorkTimeEvents.Find(Id); //searches for WorktimeEvent with given Id in the DB
            if (User.IsInRole("Admin"))
            {
                var originalStartTime = EventToUpdate.Start.ToString();
                var originalEndTime = EventToUpdate.End.ToString();
                TryUpdateModel(EventToUpdate, "",
         new string[] { "Start", "End", "Note" });
                EventToUpdate.ReadOnlyNote = "Your hours from " + originalStartTime + " - " + originalEndTime + " were changed to " + EventToUpdate.Start.ToString() + " - " + EventToUpdate.End.ToString();
                try
                {
                    //Creating a message for user by passing in ReadOnlyNote, the User ID, and the Event to the CreateMessage method in MessageController
                    Message message = MessageController.CreateMessage(EventToUpdate.ReadOnlyNote, db.Users.Find(User.Identity.GetUserId()), EventToUpdate.User, EventToUpdate);
                    db.Messages.Add(message);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException) //dex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.

                }
            }
            else User.IsInRole("User");
            {
                TryUpdateModel(EventToUpdate, "",
         new string[] { "Note" });
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.

                }
            }
                ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");
            return View(Id);
        }
