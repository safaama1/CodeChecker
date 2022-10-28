using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheckerClient.Models
{
    internal class AddLectuerer
    {
        [Required]
        [JsonProperty("teacherID")]
        public string teacherID { get; set; }

        [MaxLength(256)]
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
