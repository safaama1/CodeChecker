using System;
using System.Collections.Generic;

namespace CodeCheckerClient.MVVM.Model
{
    public class HomeworkModel
    {
        public Guid HomeworkId { get; set; }
        public string Name { get; set; }
        public Guid? CourseId { get; set; }
        public CourseModel Course { get; set; }
        public virtual ICollection<SubmittedHomeworkModel> SubmittedHomework { get; set; }
        public virtual ICollection<HomeworkRuleModel> HomeworkRules { get; set; }
    }
}
