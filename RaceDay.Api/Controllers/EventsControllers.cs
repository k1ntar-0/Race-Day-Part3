using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Services;
using RaceDay.Api.Models; // Or your entity/DTO namespace

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DbContext _context;
        private readonly IBlobStorageService _blobStorageService;

        public EventsController(DbContext context, IBlobStorageService blobStorageService)
        {
            _context = context;
            _blobStorageService = blobStorageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] Event eventItem, IFormFile? bannerImage)
        {
            // Handle image upload through Azure Blob Storage service
            if (bannerImage != null && bannerImage.Length > 0)
            {
                var imageUrl = await _blobStorageService.UploadFileAsync(bannerImage);
                var imageUrlProperty = typeof(Event).GetProperty("ImageUrl");
                if (imageUrlProperty?.CanWrite == true)
                {
                    imageUrlProperty.SetValue(eventItem, imageUrl);
                }
            }

            _context.Set<Event>().Add(eventItem);
            await _context.SaveChangesAsync();

            return Ok(eventItem);
        }
    }
}