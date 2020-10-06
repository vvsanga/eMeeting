using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMeeting.Model
{
    public class Base
    {
        [Key()]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        //[Column(TypeName = "bit")]
        //[Display(Name = "Record Status")]
        //public bool RecordStatus { get; set; }

        //[Column(TypeName = "smalldatetime")]
        //[Display(Name = "Created Date")]
        //public DateTime? CreatedDate { get; set; }

        //[Column(TypeName = "int")]
        //[Display(Name = "Created By")]
        //public int? CreatedBy { get; set; }
    }
}
