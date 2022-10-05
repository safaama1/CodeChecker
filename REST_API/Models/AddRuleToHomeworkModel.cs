using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddRuleToHomeworkModel
    {
        [Required]
        public Guid HomeworkRuleId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Range(0, 100)]
        public double Points { get; set; }
    }
}
