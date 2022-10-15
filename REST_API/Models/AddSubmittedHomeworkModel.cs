using System.ComponentModel.DataAnnotations;

namespace REST_API.Models
{
    public class AddSubmittedHomeworkModel
    {
        [Required]
        public string StudentId { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
