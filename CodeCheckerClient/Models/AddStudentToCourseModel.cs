using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CodeCheckerClient.Models
{
    public class AddStudentToCourseModel
    {
        [Required]
        [JsonProperty("studentId")]
        public string StudentId { get; set; }

        [MaxLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
