using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddHomeworkToCourseModel
    {
        [Required]
        [MaxLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
