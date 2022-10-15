using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Repositories.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentAsync(string id)
        {
            var student = await _studentRepository.GetStudentAsync(id).ConfigureAwait(false);
            if (student == null) return NotFound($"Student not found with id = {id}");
            return Ok(student);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateStudentAsync([FromBody] AddStudentToCourseModel student)
        {
            if (string.IsNullOrEmpty(student.Name))
                return BadRequest("Student name is required");
            if (string.IsNullOrEmpty(student.StudentId))
                return BadRequest("Student ID is required");
            await _studentRepository
                .CreateStudentAsync(new Student { StudentId = student.StudentId, Name = student.Name })
                .ConfigureAwait(false); ;
            return Created("~api/create", student);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateStudentAsync([FromBody] Student student)
        {
            if (string.IsNullOrEmpty(student.Name))
                return BadRequest("Student name is required");
            if (string.IsNullOrEmpty(student.StudentId))
                return BadRequest("Student ID is required");
            await _studentRepository
                .UpdateStudentAsync(student)
                .ConfigureAwait(false); ;
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacherAsync(string id)
        {
            try
            {
                await _studentRepository.DeleteStudentAsync(id).ConfigureAwait(false);
                return Ok($"Student with id = {id}, is successfully deleted");

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
