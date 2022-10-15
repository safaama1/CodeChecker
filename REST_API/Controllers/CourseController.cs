using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Repositories.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CourseController(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCoursesAsync()
        {
            return Ok(await _courseRepository.GetAllCoursesAsync().ConfigureAwait(false));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseAsync(Guid id)
        {
            var course = await _courseRepository.GetCourseAsync(id).ConfigureAwait(false);
            if (course == null) return NotFound($"Course found with id = {id}");
            return Ok(course);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCourseAsync([FromBody] AddCourseModel course)
        {
            if (string.IsNullOrEmpty(course.Name))
                return BadRequest("Course name is required");
            if (string.IsNullOrEmpty(course.TeacherID.ToString()))
                return BadRequest("Teacher ID is required");
            try
            {
                var teacherEntity = await _teacherRepository.GetTeacherAsync(course.TeacherID).ConfigureAwait(false);
                var newCourseId = Guid.NewGuid();
                await _courseRepository
                    .CreateCourseAsync(new Course
                    {
                        CourseId = newCourseId,
                        Name = course.Name,
                        AcademicYear = course.AcademicYear,
                        TeacherId = teacherEntity.TeacherId,
                        Teacher = teacherEntity
                    })
                    .ConfigureAwait(false); ;
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpPut("{id}/add-student")]
        public async Task<ActionResult> AddStudentToCourseAsync(Guid id, [FromBody] AddStudentToCourseModel student)
        {
            if (string.IsNullOrEmpty(student.StudentId.ToString())) return BadRequest("Student ID is required");
            var selectedCourse = await _courseRepository.GetCourseAsync(id).ConfigureAwait(false);
            if (selectedCourse == null) return NotFound($"Course with id = {id} not found");
            await _courseRepository.AddStudentToCourseAsync(new Student { StudentId = student.StudentId, Name = student.Name }, selectedCourse).ConfigureAwait(false);
            return Accepted();
        }

        [HttpPut("{id}/add-homework")]
        public async Task<ActionResult> AddHomeworkToCourseAsync(Guid id, [FromBody] AddHomeworkToCourseModel homework)
        {
            var selectedCourse = await _courseRepository.GetCourseAsync(id).ConfigureAwait(false);
            if (selectedCourse == null) return NotFound($"Course with id = {id} not found");
            var newHomeworkId = Guid.NewGuid();
            await _courseRepository.AddHomeworkToCourseAsync(new Homework { HomeworkId = newHomeworkId, Name = homework.Name }, selectedCourse).ConfigureAwait(false);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseAsync(Guid id)
        {
            try
            {
                await _courseRepository.DeleteCourseAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Accepted();
        }
    }
}
