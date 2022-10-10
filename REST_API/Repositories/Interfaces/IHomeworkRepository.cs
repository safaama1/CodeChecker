using DataAccessLayer.Entities;

namespace REST_API.Repositories.Interfaces
{
    public interface IHomeworkRepository
    {
        // Get methods
        Task<IEnumerable<Homework>> GetAllHomeworkAsync();
        Task<Homework> GetHomeworkAsync(Guid id);
        Task<IEnumerable<HomeworkRule>> GetHomeworkRulesAsync(Guid id);
        Task<IEnumerable<SubmittedHomework>> GetHomeworkSubmits(Guid id);

        // Post methods
        Task<Homework> CreateHomeworkAsync(Homework homework);
        Task<HomeworkRule> CreateHomeworkRuleAsync(HomeworkRule rule);
        Task<IEnumerable<HomeworkRule>> CreateHomeworkRulesAsync(IEnumerable<HomeworkRule> rules);
        Task<SubmittedHomework> CreateSubmittedHomeworkAsync(SubmittedHomework submittedHomework);

        // Put methods
        Task UpdateHomeworkAsync(Homework homework);
        Task UpdateHomeworkRuleAsync(HomeworkRule rule);
        Task UpdateSubmittedHomeworkAsync(SubmittedHomework submittedHomework);
        Task AddRuleToHomework(HomeworkRule rule, Guid homeworkId);

        // Delete methods
        Task DeleteHomeworkAsync(Guid id);
        Task DeleteHomeworkRuleAsync(Guid id);
        Task DeleteSubmittedHomeworkAsync(Guid id);

    }
}
