using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }
        [Required]
        public int EnrolmentId { get; set; }
        [ForeignKey("EnrolmentId")]
        public EventEnrolment? Enrolment { get; set; }
    }
}