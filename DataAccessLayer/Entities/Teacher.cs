using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Teacher
    {
        [Key]
        public Guid TeacherId { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
