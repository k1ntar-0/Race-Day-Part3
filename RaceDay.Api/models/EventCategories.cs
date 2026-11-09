using System.ComponentModel.DataAnnotations;

namespace RaceDay.Api.Models
{
    public class EventCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string EventType { get; set; } = string.Empty;
    }
}