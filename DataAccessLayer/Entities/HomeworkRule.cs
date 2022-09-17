using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class HomeworkRule
    {
        [Key]
        public Guid HomeworkRuleId { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Range(0, 100, ErrorMessage = "Number of points must be between {1} and {2}.")]
        public double Points { get; set; }

        [ForeignKey("Homework")]
        public Guid? HomeworkId { get; set; }
        public Homework Homework { get; set; }

    }
}
