using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddCourseModel
    {
        [MaxLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }

        [MaxLength(256)]
        [JsonProperty("academicYear")]
        public string AcademicYear { get; set; }

    }
}
