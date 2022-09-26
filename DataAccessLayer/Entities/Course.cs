using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string AcademicYear { get; set; }

        [ForeignKey("Teacher")]
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Homework> Homework { get; set; }

    }
}
