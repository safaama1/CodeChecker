using System;
using System.Collections.Generic;

namespace CodeCheckerClient.MVVM.Model
{
    public class CourseModel
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string AcademicYear { get; set; }
        public string TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }
        public virtual ICollection<StudentModel> Students { get; set; }
        public virtual ICollection<HomeworkModel> Homework { get; set; }

    }
}
