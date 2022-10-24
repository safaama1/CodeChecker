using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using REST_API.Repositories.Interfaces;

namespace REST_API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CodeCheckerDbContext _context;

        public CourseRepository(CodeCheckerDbContext context)
        {
            _context = context;
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return course;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id).ConfigureAwait(false);
            if (courseToDelete != null)
            {
                _context.Courses.Remove(courseToDelete);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else throw new KeyNotFoundException("Course not found");

        }

        public async Task<IEnumerable<Homework>?> GetCourseHomeworkAsync(Guid id)
        {
            var course = await _context.Courses
                .Where(course => course.CourseId == id)
                .Include(course => course.Homework)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (course != null) return course.Homework;
            else throw new KeyNotFoundException("Course not found");
        }

        public async Task<IEnumerable<Course>?> GetAllCoursesByStudentIdAsync(string id)
        {
            var studentEntity = await _context.Students
                .Where(s => s.StudentId == id)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (studentEntity != null) return studentEntity.Courses;
            else throw new KeyNotFoundException("Student not found");
        }

        public async Task<IEnumerable<Course>?> GetAllCoursesByTeacherIdAsync(string id)
        {
            var teacherEntity = await _context.Teachers
                .Where(t => t.TeacherId == id)
                .Include(t => t.Courses)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (teacherEntity != null) return teacherEntity.Courses;
            else throw new KeyNotFoundException("Teacher not found");
        }

        public async Task<IEnumerable<Course>?> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(course => course.Teacher)
                .Include(course => course.Students)
                .Include(course => course.Homework)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Course?> GetCourseAsync(Guid id)
        {
            var course = await _context.Courses
                .Where(course => course.CourseId == id)
                .Include(course => course.Students)
                .Include(course => course.Teacher)
                .Include(course => course.Homework)
                .ThenInclude(h => h.SubmittedHomework)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (course != null) return course;
            else throw new KeyNotFoundException("Course not found");
        }

        public async Task<IEnumerable<Student>?> GetCourseStudentsAsync(Guid id)
        {
            var course = await _context.Courses
                .Where(course => course.CourseId == id)
                .Include(course => course.Students)
                .Include(course => course.Homework)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (course != null) return course.Students;
            else throw new KeyNotFoundException("Course not found");
        }

        public async Task<Teacher?> GetCourseTeacherAsync(Guid id)
        {
            var course = await _context.Courses
                .Where(course => course.CourseId == id)
                .Include(course => course.Teacher)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (course != null) return course.Teacher;
            else throw new KeyNotFoundException("Course not found");
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var courseEntity = await _context.Courses
                .Where(c => c.CourseId == course.CourseId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (courseEntity == null)
                throw new KeyNotFoundException("Course not found");
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }


        public async Task AddStudentToCourseAsync(Student student, Course course)
        {
            var studentEntity = await _context.Students
                .Where(s => s.StudentId == student.StudentId)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync();
            var courseEntity = await _context.Courses
                .Where(c => c.CourseId == course.CourseId)
                .Include(c => c.Students)
                .FirstOrDefaultAsync();
            if (courseEntity == null) throw new KeyNotFoundException("Course not found");
            if (studentEntity == null)
            {
                student.Courses = new List<Course>();
                await _context.Students.AddAsync(student).ConfigureAwait(false);
            }
            else student = studentEntity;
            student.Courses.Add(courseEntity);
            if (courseEntity.Students == null) courseEntity.Students = new List<Student>();
            courseEntity.Students.Add(student);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task AddHomeworkToCourseAsync(Homework homework, Course course)
        {
            var homeworkEntity = await _context.Homework
                .Where(h => h.HomeworkId == homework.HomeworkId)
                .FirstOrDefaultAsync();
            var courseEntity = await _context.Courses
                .Where(c => c.CourseId == course.CourseId)
                .Include(c => c.Students)
                .FirstOrDefaultAsync();

            if (courseEntity == null) return;
            homework.CourseId = course.CourseId;
            homework.Course = course;
            if (homeworkEntity == null)
                await _context.Homework.AddAsync(homework).ConfigureAwait(false);
            if (courseEntity.Students == null) courseEntity.Homework = new List<Homework>();
            courseEntity.Homework.Add(homework);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
