using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace eMeeting.Model
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }
    }
}
