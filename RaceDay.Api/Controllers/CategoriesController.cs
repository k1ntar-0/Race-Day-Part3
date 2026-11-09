using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public CategoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.EventCategories.ToListAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] EventCategory category)
        {
            string? role = HttpContext.Session.GetString("UserRole");
            if (role != "Organiser")
                return Unauthorized(new { message = "Only Organisers can create categories." });

            _context.EventCategories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category created successfully." });
        }
    }
}