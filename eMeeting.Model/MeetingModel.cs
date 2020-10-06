using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace eMeeting.Model
{
    public class MeetingModel : Base
    {
        public string Subject { get; set; }

        public string Agenda { get; set; }

        public DateTime Schedule { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

    }
}
