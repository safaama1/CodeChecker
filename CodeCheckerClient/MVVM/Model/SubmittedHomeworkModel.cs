using System;

namespace CodeCheckerClient.MVVM.Model
{
    public class SubmittedHomeworkModel
    {
        public Guid SubmittedHomeworkId { get; set; }
        public double Grade { get; set; }
        public DateTime SubmittedDate { get; set; }
        public Guid HomeworkId { get; set; }
        public HomeworkModel Homework { get; set; }
        public string StudentId { get; set; }
        public StudentModel Student { get; set; }
    }
}
