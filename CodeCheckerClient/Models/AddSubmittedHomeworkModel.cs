using System;
using System.ComponentModel.DataAnnotations;

namespace CodeCheckerClient.Models
{
    public class AddSubmittedHomeworkModel
    {
        [Required]
        public string StudentId { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
