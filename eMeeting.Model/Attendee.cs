using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMeeting.Model
{
    public class Attendee : Base
    {
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
