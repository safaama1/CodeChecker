using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddTeacherModel
    {
        [Required]
        [MaxLength(256)]
        [JsonProperty("teacherId ")]
        public string TeacherID { get; set; }

        [MaxLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
