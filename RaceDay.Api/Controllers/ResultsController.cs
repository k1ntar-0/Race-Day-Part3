using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] Result result)
        {
            string? role = HttpContext.Session.GetString("UserRole");
            if (role != "Organiser")
                return Unauthorized(new { message = "Only Organisers can capture race results." });

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Race result captured successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e!.Event)
                .Include(r => r.Enrolment)
                .ThenInclude(e => e!.Participant)
                .ToListAsync();

            return Ok(results);
        }
    }
}