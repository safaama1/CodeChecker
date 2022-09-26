using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class SubmittedHomework
    {
        [Key]
        public Guid SubmittedHomeworkId { get; set; }

        [Range(0, 100, ErrorMessage = "Grade must be between {1} and {2}.")]
        public double Grade { get; set; }

        public DateTime SubmittedDate { get; set; }

        [ForeignKey("Homework")]
        public Guid HomeworkId { get; set; }
        public Homework Homework { get; set; }

        [ForeignKey("Student")]
        public string? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
