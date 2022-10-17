using DataAccessLayer.Entities;

namespace REST_API.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        // Get methods 
        Task<Student> GetStudentAsync(string id);

        // Post methods
        Task<Student> CreateStudentAsync(Student student);

        // Put methods
        Task UpdateStudentAsync(Student student);

        // Delete methods
        Task DeleteStudentAsync(string id);
    }
}
