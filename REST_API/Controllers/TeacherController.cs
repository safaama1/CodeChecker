using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Repositories.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherAsync(string id)
        {
            var teacher = await _teacherRepository.GetTeacherAsync(id).ConfigureAwait(false);
            if (teacher == null) return NotFound($"Teacher not found with id = {id}");
            return Ok(teacher);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateTeacherAsync([FromBody] AddTeacherModel teacher)
        {
            if (string.IsNullOrEmpty(teacher.Name))
                return BadRequest("Teacher name is required");
            if (string.IsNullOrEmpty(teacher.TeacherID))
                return BadRequest("Teacher ID is required");
            await _teacherRepository
                .CreateTeacherAsync(new Teacher { TeacherId = teacher.TeacherID, Name = teacher.Name })
                .ConfigureAwait(false); ;
            return Created("~api/create", teacher);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateTeacherAsync([FromBody] Teacher teacher)
        {
            if (string.IsNullOrEmpty(teacher.Name))
                return BadRequest("Teacher name is required");
            if (string.IsNullOrEmpty(teacher.TeacherId))
                return BadRequest("Teacher ID is required");
            await _teacherRepository
                .UpdateTeacherAsync(teacher)
                .ConfigureAwait(false); ;
            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacherAsync(string id)
        {
            try
            {
                await _teacherRepository.DeleteTeacherAsync(id).ConfigureAwait(false);
                return Ok($"Teacher with id = {id}, is successfully deleted");

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
