using DataAccessLayer.Entities;

namespace REST_API.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        // Get methods 
        Task<Teacher> GetTeacherAsync(string id);

        // Post methods
        Task<Teacher> CreateTeacherAsync(Teacher teacher);

        // Put methods
        Task UpdateTeacherAsync(Teacher teacher);

        // Delete methods
        Task DeleteTeacherAsync(string id);
    }
}
