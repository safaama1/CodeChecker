using DataAccessLayer.Entities;

namespace REST_API.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        // Get methods
        Task<IEnumerable<Course>?> GetAllCoursesAsync();
        Task<IEnumerable<Course>?> GetAllCoursesByStudentIdAsync(string id);
        Task<IEnumerable<Course>?> GetAllCoursesByTeacherIdAsync(string id);
        Task<Course?> GetCourseAsync(Guid id);
        Task<Teacher?> GetCourseTeacherAsync(Guid id);
        Task<IEnumerable<Student>?> GetCourseStudentsAsync(Guid id);
        Task<IEnumerable<Homework>?> GetCourseHomeworkAsync(Guid id);

        // Post methods
        Task<Course> CreateCourseAsync(Course course);

        // Put methods
        Task UpdateCourseAsync(Course course);
        Task AddStudentToCourseAsync(Student student, Course course);
        Task AddHomeworkToCourseAsync(Homework homework, Course course);
        // Delete methods
        Task DeleteCourseAsync(Guid id);
    }
}
