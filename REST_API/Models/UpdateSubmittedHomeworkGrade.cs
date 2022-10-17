using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class UpdateSubmittedHomeworkGrade
    {
        [Required]
        [Range(0, 100, ErrorMessage = "Grade must be between {1} and {2}.")]
        public double Grade { get; set; }
    }
}
