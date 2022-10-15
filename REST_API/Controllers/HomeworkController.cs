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
            if (course == null) return NotFound($"Homework not found with id = {id}");
            return Ok(course);
        }

        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<Homework>>> CreateHomeworkAsync(AddHomeworkToCourseModel model)
        {
            var hwId = new Guid();
            var course = await _homeworkRepository.CreateHomeworkAsync(new Homework { HomeworkId = hwId, Name = model.Name, CourseId = model.CourseId }).ConfigureAwait(false);
            return Ok(course);
        }

        [HttpPut("{id}/add-rule")]
        public async Task<ActionResult> AddRuleToHomeworkAsync(Guid id, [FromBody] AddRuleToHomeworkModel addRuleToHomework)
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

        [HttpPut("{id}/submitted-homework/{sId}/update-grade")]
        public async Task<ActionResult> UpdateSubmittedHomework(Guid id, Guid sId, UpdateSubmittedHomeworkGrade homeworkGrade)
        {
            try
            {
                var course = await _homeworkRepository.GetHomeworkAsync(id).ConfigureAwait(false);
                if (course == null) return NotFound($"Homework not found with id = {id}");

                await _homeworkRepository.UpdateSubmittedHomeworkGradeAsync(sId, homeworkGrade.Grade).ConfigureAwait(false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/add-submitted-homework")]
        public async Task<ActionResult> AddSubmitToHomeworkAsync(Guid id, AddSubmittedHomeworkModel addSubmittedHomework)
        {
            if (string.IsNullOrEmpty(addSubmittedHomework.StudentId))
                return BadRequest("The student ID that submitted the homework is required");
            var submitted = new SubmittedHomework
            {
                SubmittedHomeworkId = new Guid(),
                SubmittedDate = addSubmittedHomework.SubmittedDate,
            };
            try
            {
                await _homeworkRepository.AddNewSubmitToHomework(submitted, id, addSubmittedHomework.StudentId).ConfigureAwait(false);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHomeworkAsync(Guid id)
        {
            try
            {
                await _homeworkRepository.DeleteHomeworkAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Accepted();
        }

    }
}
