using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using REST_API.Repositories.Interfaces;

namespace REST_API.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly CodeCheckerDbContext _context;

        public StudentRepository(CodeCheckerDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            if (student == null) throw new ArgumentNullException("Student info is required");
            await _context.Students.AddAsync(student).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return student;
        }

        public async Task DeleteStudentAsync(string id)
        {
            var studentToDelete = await _context.Students.FindAsync(id).ConfigureAwait(false);
            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Student not found");
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            var student = await _context.Students
                             .Where(s => s.StudentId == id)
                             .Include(s => s.Courses)
                             .Include(s => s.SubmittedHomework)
                             .FirstOrDefaultAsync()
                             .ConfigureAwait(false);
            if (student == null) throw new KeyNotFoundException("Student not found");
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var studentEntity = await _context.Students
               .Where(s => s.StudentId == student.StudentId)
               .FirstOrDefaultAsync()
               .ConfigureAwait(false);
            if (studentEntity == null)
                throw new KeyNotFoundException("Student not found");
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
