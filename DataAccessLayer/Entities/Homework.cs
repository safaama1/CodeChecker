using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Homework
    {
        [Key]
        public Guid HomeworkId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [ForeignKey("Course")]
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }

        public List<SubmittedHomework>? SubmittedHomework { get; set; }

        public List<HomeworkRule>? HomeworkRules { get; set; }
    }
}
