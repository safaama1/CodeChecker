using System.Collections.Generic;

namespace CodeCheckerClient.MVVM.Model
{
    public class StudentModel
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourseModel> Courses { get; set; }
        public virtual ICollection<SubmittedHomeworkModel> SubmittedHomework { get; set; }
    }
}
