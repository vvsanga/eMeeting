using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace eMeeting.Model
{
    public class Meeting : Base
    {
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Subject { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Agenda { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Schedule")]
        public DateTime Schedule { get; set; }

        [Display(Name = "Meeting Attendees")]
        public virtual ICollection<MeetingAttendee> MeetingAttendees  { get; set; }

        [NotMapped]
        [Display(Name = "Attendees")]
        public string Attendees
        {
            get
            {
                return MeetingAttendees != null && MeetingAttendees.Any() && MeetingAttendees.ToArray()[0].Attendee != null?
                                MeetingAttendees
                                .Where(i => i.Attendee != null)
                                .Select(i => i.Attendee.Name)
                                .Aggregate((item, next) => item + ", " + next)
                                : "";
            }
        }
    }
}

//.FirstOrDefault())
//.TakeWhile(i => i.Attendee != null)
