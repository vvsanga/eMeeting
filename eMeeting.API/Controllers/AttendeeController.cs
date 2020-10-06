using eMeeting.API.Data;
using eMeeting.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace eMeeting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors(origins: "http://emeeting.dsolit.com", headers: "*", methods: "*")]
    public class AttendeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendeeController(ApplicationDbContext context)
        {
            _context = context;
        }      

        // GET: api/Attendee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendee>>> Get()
        {
            var result = await _context.Attendees.ToListAsync();
            return result.ToArray();
        }

        //// GET: api/Attendee/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Attendee>> GetAttendee(int id)
        //{
        //    var attendee = await _context.Attendees.FindAsync(id);

        //    if (attendee == null)
        //    {
        //        return NotFound();
        //    }

        //    return attendee;
        //}

        //// PUT: api/Attendee/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAttendee(int id, Attendee attendee)
        //{
        //    if (id != attendee.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(attendee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AttendeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Attendee
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Attendee>> Post(Attendee attendee)
        {
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = attendee.Id }, attendee);
        }

        //// DELETE: api/Attendee/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Attendee>> DeleteAttendee(int id)
        //{
        //    var attendee = await _context.Attendees.FindAsync(id);
        //    if (attendee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Attendees.Remove(attendee);
        //    await _context.SaveChangesAsync();

        //    return attendee;
        //}

        //private bool AttendeeExists(int id)
        //{
        //    return _context.Attendees.Any(e => e.Id == id);
        //}
    }
}
