using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddRuleToHomeworkModel
    {
        [Required]
        [JsonProperty("homeworkRuleId")]
        public Guid HomeworkRuleId { get; set; }

        [Required]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [Range(0, 100)]
        [JsonProperty("points")]
        public double Points { get; set; }
    }
}
