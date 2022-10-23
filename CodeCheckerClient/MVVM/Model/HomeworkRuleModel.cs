using System;

namespace CodeCheckerClient.MVVM.Model
{
    public class HomeworkRuleModel
    {
        public Guid HomeworkRuleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Points { get; set; }
        public Guid? HomeworkId { get; set; }
        public HomeworkModel Homework { get; set; }
    }
}
