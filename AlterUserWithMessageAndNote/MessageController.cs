        //POST: Message/Create START HERE
        //[HttpPost, ActionName("Edit")]
        [Authorize(Roles = "Admin,User")]
        public static  Message CreateMessage (string messageNote, ApplicationUser sender, ApplicationUser recipient, Event x)
        {
            Message message = new Message();
            message.MessageID = Guid.NewGuid();
            message.DateSent = DateTime.Now;
            message.MessageTitle = "Work Period Updated";
            message.Content = messageNote;
            message.Sender = sender;
            message.Recipient = recipient;
            //message.EventID = x;
            return message;
        }
