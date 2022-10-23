using System.Collections.Generic;

namespace CodeCheckerClient.MVVM.Model
{
    public class TeacherModel
    {
        public string TeacherId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseModel> Courses { get; set; }
    }
}
