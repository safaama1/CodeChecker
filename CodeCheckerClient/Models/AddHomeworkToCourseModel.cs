using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCheckerClient.Models
{
    public class AddHomeworkToCourseModel
    {
        [Required]
        [MaxLength(256)]
        [JsonProperty("name")]
        public string Name { get; set; }

        public Guid? CourseId { get; set; }
    }
}
