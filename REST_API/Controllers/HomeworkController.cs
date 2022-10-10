using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using REST_API.Models;
using REST_API.Repositories.Interfaces;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkController(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomeworkAsync(Guid id)
        {
            var course = await _homeworkRepository.GetHomeworkAsync(id).ConfigureAwait(false);
            if (course == null) return NotFound($"Homework found with id = {id}");
            return Ok(course);
        }

        //[HttpPost("create-homework")]
        //public async Task<ActionResult> CreateHomeworkAsync([FromBody] AddHomeworkToCourseModel addHomeworkModel)
        //{
        //    if (string.IsNullOrEmpty(addHomeworkModel.CourseId.ToString()))
        //        return BadRequest("Course name is required");
        //    var newHomeworkId = Guid.NewGuid();
        //    await _courseRepository
        //        .CreateCourseAsync(new Course { CourseId = newCourseId, Name = course.Name, AcademicYear = course.AcademicYear })
        //        .ConfigureAwait(false); ;
        //    return Accepted();
        //}

        //[HttpPost("create-rule")]
        //public async Task<ActionResult> CreateHomeworkRuleAsync([FromBody] AddHomeworkRuleModel addHomeworkRuleModel)
        //{
        //    if (string.IsNullOrEmpty(course.Name))
        //        return BadRequest("Course name is required");
        //    var newCourseId = Guid.NewGuid();
        //    await _courseRepository
        //        .CreateCourseAsync(new Course { CourseId = newCourseId, Name = course.Name, AcademicYear = course.AcademicYear })
        //        .ConfigureAwait(false); ;
        //    return Accepted();
        //}

        //[HttpPost("create-submitted")]
        //public async Task<ActionResult> CreateSubmittedHomeworkAsync([FromBody] AddSubmittedHomeworkModel addSubmittedHomeworkModel)
        //{
        //    if (string.IsNullOrEmpty(course.Name))
        //        return BadRequest("Course name is required");
        //    var newCourseId = Guid.NewGuid();
        //    await _courseRepository
        //        .CreateCourseAsync(new Course { CourseId = newCourseId, Name = course.Name, AcademicYear = course.AcademicYear })
        //        .ConfigureAwait(false); ;
        //    return Accepted();
        //}

        [HttpPut("{id}/add-rule")]
        public async Task<ActionResult> AddRuleToHomeworkAsync(Guid id, AddRuleToHomeworkModel addRuleToHomework)
        {
            var rule = new HomeworkRule
            {
                HomeworkRuleId = addRuleToHomework.HomeworkRuleId,
                Title = addRuleToHomework.Title,
                Description = addRuleToHomework.Description,
                Points = addRuleToHomework.Points
            };
            try
            {
                await _homeworkRepository.AddRuleToHomework(rule, id).ConfigureAwait(false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
