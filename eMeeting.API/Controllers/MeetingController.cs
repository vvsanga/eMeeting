using AutoMapper;
using eMeeting.API.Data;
using eMeeting.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace eMeeting.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors(origins: "http://emeeting.dsolit.com", headers: "*", methods: "*")]
    public class MeetingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MeetingController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Meeting
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meeting>>> Get()
        {
            var result = await _context.Meetings
                            .Include(e => e.MeetingAttendees)
                            .Include("MeetingAttendees.Attendee")
                            .ToListAsync();
            return result.ToArray();
        }

        //// GET: api/Meeting
        //[HttpGet]
        //public IEnumerable<Meeting> Get()
        //{
        //    var result = _context.Meetings.ToList();
        //    return result.ToArray();
        //}

        // GET: api/Meeting/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meeting>> Get(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return meeting;
        }

        // POST: api/Meeting
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Meeting>> Post(MeetingModel meetingModel)
        {
            var meeting = _mapper.Map<Meeting>(meetingModel);
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();

            foreach (Attendee attendee in meetingModel.Attendees)
            {
                _context.MeetingAttendees.Add(new MeetingAttendee { MeetingId = meeting.Id, AttendeeId = attendee.Id });
            }
            await _context.SaveChangesAsync();


            return CreatedAtAction("Get", new { id = meeting.Id }, meeting);
        }

        // PUT: api/Meeting/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Meeting meeting)
        {
            if (id != meeting.Id)
            {
                return BadRequest();
            }

            _context.Entry(meeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Meeting/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Meeting>> Delete(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();

            return meeting;
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.Id == id);
        }
    }
}
