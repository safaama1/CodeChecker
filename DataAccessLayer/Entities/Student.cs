using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Student
    {
        [Key]
        public string StudentId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<SubmittedHomework> SubmittedHomework { get; set; }
    }
}
