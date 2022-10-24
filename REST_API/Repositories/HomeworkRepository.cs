using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using REST_API.Repositories.Interfaces;

namespace REST_API.Repositories
{
    public class HomeworkRepository : IHomeworkRepository
    {
        private readonly CodeCheckerDbContext _context;

        public HomeworkRepository(CodeCheckerDbContext context)
        {
            _context = context;
        }

        public async Task AddRuleToHomework(HomeworkRule rule, Guid homeworkId)
        {
            var homeworkEntity = _context.Homework.Where(h => h.HomeworkId == homeworkId).Include(h => h.HomeworkRules).FirstOrDefault();
            if (homeworkEntity == null) throw new KeyNotFoundException("Homework not found");
            rule.HomeworkId = homeworkId;
            rule.Homework = homeworkEntity;
            homeworkEntity.HomeworkRules.Add(rule);
            var ruleEntity = _context.HomeworkRules.Where(r => r.HomeworkRuleId == rule.HomeworkRuleId).FirstOrDefault();
            if (ruleEntity == null) _context.HomeworkRules.Add(rule);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<SubmittedHomework> AddNewSubmitToHomework(SubmittedHomework submitted, Guid homeworkId, string studentId)
        {
            var homeworkEntity = _context.Homework.Where(h => h.HomeworkId == homeworkId).Include(h => h.SubmittedHomework).FirstOrDefault();
            if (homeworkEntity == null) throw new KeyNotFoundException("Homework not found");
            var studentEntity = _context.Students.Where(s => s.StudentId == studentId).Include(h => h.SubmittedHomework).FirstOrDefault();
            if (studentEntity == null) throw new KeyNotFoundException("Student not found");
            // update the submitted homework connected entities 
            submitted.HomeworkId = homeworkId;
            submitted.Homework = homeworkEntity;
            submitted.StudentId = studentId;
            submitted.Student = studentEntity;
            // add the submitted homework to the student & homework enitities 
            homeworkEntity.SubmittedHomework.Add(submitted);
            studentEntity.SubmittedHomework.Add(submitted);
            var submittedEntity = _context.SubmittedHomework.Where(s => s.SubmittedHomeworkId == submitted.SubmittedHomeworkId).FirstOrDefault();
            if (submittedEntity == null) _context.SubmittedHomework.Add(submitted);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return submitted;
        }

        public async Task<Homework> CreateHomeworkAsync(Homework homework)
        {
            await _context.Homework.AddAsync(homework).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return homework;
        }

        public async Task<HomeworkRule> CreateHomeworkRuleAsync(HomeworkRule rule)
        {
            await _context.HomeworkRules.AddAsync(rule).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return rule;
        }

        public async Task<IEnumerable<HomeworkRule>> CreateHomeworkRulesAsync(IEnumerable<HomeworkRule> rules)
        {
            await _context.HomeworkRules.AddRangeAsync(rules).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return rules;
        }

        public async Task<SubmittedHomework> CreateSubmittedHomeworkAsync(SubmittedHomework submittedHomework)
        {
            await _context.SubmittedHomework.AddAsync(submittedHomework).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return submittedHomework;
        }

        public async Task DeleteHomeworkAsync(Guid id)
        {
            var homeworkToDelete = await _context.Homework.FindAsync(id).ConfigureAwait(false);
            if (homeworkToDelete != null)
            {
                _context.Homework.Remove(homeworkToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Homework not found");
        }

        public async Task DeleteHomeworkRuleAsync(Guid id)
        {
            var homeworkRuleToDelete = await _context.HomeworkRules.FindAsync(id).ConfigureAwait(false);
            if (homeworkRuleToDelete != null)
            {
                _context.HomeworkRules.Remove(homeworkRuleToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Homework rule not found");
        }

        public async Task DeleteSubmittedHomeworkAsync(Guid id)
        {
            var submittedHomeworkToDelete = await _context.SubmittedHomework.FindAsync(id).ConfigureAwait(false);
            if (submittedHomeworkToDelete != null)
            {
                _context.SubmittedHomework.Remove(submittedHomeworkToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Submitted homework not found");
        }

        public async Task<IEnumerable<Homework>> GetAllHomeworkAsync()
        {
            return await _context.Homework
                  .Include(homework => homework.HomeworkRules)
                  .Include(homework => homework.SubmittedHomework)
                  .ThenInclude(submit => submit.Student)
                  .ToListAsync()
                  .ConfigureAwait(false);
        }
        public async Task<Homework> GetHomeworkAsync(Guid id)
        {
            var homework = await _context.Homework
                  .Where(homework => homework.HomeworkId == id)
                  .Include(homework => homework.HomeworkRules)
                  .Include(homework => homework.SubmittedHomework)
                  .ThenInclude(submit => submit.Student)
                  .FirstOrDefaultAsync()
                  .ConfigureAwait(false);
            if (homework == null) throw new KeyNotFoundException("Homework not found");
            return homework;
        }

        public async Task<IEnumerable<HomeworkRule>> GetHomeworkRulesAsync(Guid id)
        {
            var homework = await _context.Homework
                  .Where(homework => homework.HomeworkId == id)
                  .Include(homework => homework.SubmittedHomework)
                  .Include(homework => homework.HomeworkRules)
                  .FirstOrDefaultAsync()
                  .ConfigureAwait(false);
            if (homework == null) throw new KeyNotFoundException("Homework not found");
            return homework.HomeworkRules;
        }

        public async Task<IEnumerable<SubmittedHomework>> GetHomeworkSubmits(Guid id)
        {
            var homework = await _context.Homework
                 .Where(homework => homework.HomeworkId == id)
                 .Include(homework => homework.SubmittedHomework)
                 .Include(homework => homework.HomeworkRules)
                 .FirstOrDefaultAsync()
                 .ConfigureAwait(false);
            if (homework == null) throw new KeyNotFoundException("Homework not found");
            return homework.SubmittedHomework;
        }

        public async Task UpdateHomeworkAsync(Homework homework)
        {
            var homeworkEntity = await _context.Homework
                .Where(h => h.HomeworkId == homework.HomeworkId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (homeworkEntity == null)
                throw new KeyNotFoundException("Homework not found");
            _context.Entry(homework).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateHomeworkRuleAsync(HomeworkRule rule)
        {
            var homeworkRuleEntity = await _context.HomeworkRules
                .Where(r => r.HomeworkRuleId == rule.HomeworkRuleId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (homeworkRuleEntity == null)
                throw new KeyNotFoundException("Homework rule not found");
            _context.Entry(rule).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateSubmittedHomeworkAsync(SubmittedHomework submittedHomework)
        {
            var submittedHomeworkEntity = await _context.SubmittedHomework
                .Where(s => s.SubmittedHomeworkId == submittedHomework.SubmittedHomeworkId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (submittedHomeworkEntity == null)
                throw new KeyNotFoundException("Submitted homework not found");
            _context.Entry(submittedHomework).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateSubmittedHomeworkGradeAsync(Guid submittedHomeworkId, double newGrade)
        {
            var submittedHomeworkEntity = await _context.SubmittedHomework
                .Where(s => s.SubmittedHomeworkId == submittedHomeworkId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (submittedHomeworkEntity == null)
                throw new KeyNotFoundException("Submitted homework not found");
            submittedHomeworkEntity.Grade = newGrade;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
