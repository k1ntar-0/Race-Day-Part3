using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public int OrganiserId { get; set; }
        [ForeignKey("OrganiserId")]
        public User? Organiser { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
    }
}