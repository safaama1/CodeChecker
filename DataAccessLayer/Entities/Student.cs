using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Student
    {
        [Key]
        public Guid StudentId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public List<Course> Courses { get; set; }

        public List<SubmittedHomework> SubmittedHomework { get; set; }
    }
}
