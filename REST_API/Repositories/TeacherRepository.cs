using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using REST_API.Repositories.Interfaces;

namespace REST_API.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {

        private readonly CodeCheckerDbContext _context;

        public TeacherRepository(CodeCheckerDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> CreateTeacherAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException("Teacher info is required");
            await _context.Teachers.AddAsync(teacher).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return teacher;

        }

        public async Task DeleteTeacherAsync(string id)
        {
            var teacherToDelete = await _context.Teachers.FindAsync(id).ConfigureAwait(false);
            if (teacherToDelete != null)
            {
                _context.Teachers.Remove(teacherToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Teacher not found");
        }

        public async Task<Teacher> GetTeacherAsync(string id)
        {
            var teacher = await _context.Teachers
                 .Where(t => t.TeacherId == id)
                 .Include(t => t.Courses)
                 .FirstOrDefaultAsync()
                 .ConfigureAwait(false);
            if (teacher == null) throw new KeyNotFoundException("Teacher not found");
            return teacher;
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            var teacherEntity = await _context.Teachers
                .Where(t => t.TeacherId == teacher.TeacherId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (teacherEntity == null)
                throw new KeyNotFoundException("Teacher not found");
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
