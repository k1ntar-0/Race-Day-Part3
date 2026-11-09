using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EnrolmentsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Enrol([FromBody] EventEnrolment enrolment)
        {
            string? role = HttpContext.Session.GetString("UserRole");
            if (role != "Participant")
                return Unauthorized(new { message = "Only Participants can enrol in events." });

            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Unauthorized(new { message = "User not authenticated." });

            enrolment.ParticipantId = userId.Value;

            _context.EventEnrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Enrolment successful!" });
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrolments()
        {
            var enrolments = await _context.EventEnrolments
                .Include(e => e.Event)
                .Include(e => e.Participant)
                .Include(e => e.Category)
                .ToListAsync();

            return Ok(enrolments);
        }
    }
}