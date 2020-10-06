using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMeeting.Model
{
    public class MeetingAttendee : Base
    {
        [Required]
        [ForeignKey("Meeting")]
        [Column(TypeName = "int")]
        [Display(Name = "Meeting")]
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }

        [Required]
        [ForeignKey("Attendee")]
        [Column(TypeName = "int")]
        [Display(Name = "Attendee")]
        public int AttendeeId { get; set; }

        public Attendee Attendee { get; set; }
    }
}
