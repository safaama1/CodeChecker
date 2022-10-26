using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.Models
{
    internal class AddCourseToLecturerModel
    {
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("academicYear")]
        public string AcademicYear { get; set; }

        [JsonProperty("teacherID")]
        public string TeacherID { get; set; }
    }

}
