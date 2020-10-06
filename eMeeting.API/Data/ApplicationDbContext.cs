using eMeeting.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eMeeting.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<MeetingAttendee> MeetingAttendees { get; set; }
    }
}
