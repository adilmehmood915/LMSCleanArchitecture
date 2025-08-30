using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using LMSCleanArchitecrure.Application.Features.Course.Command.DeleteCourse;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetCourseById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSCleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO dto)
        {
            var command = new CreateCourseCommand(dto);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var command = new DeleteCourseCommand(id);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCourses()
        {
            var query = new GetAllCoursesQuery();
            var courses = await _mediator.Send(query);
            return Ok(courses);
        }

        [HttpGet("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var query = new GetCourseByIdQuery(id);
            var course = await _mediator.Send(query);
            if (course == null)
                return NotFound();
            return Ok(course);
        }
        [HttpPost("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> UpdateCourseAsync(int id , UpdateCourseDTO dto)
        {
            var command = new UpdateCourseCommand(id, dto);
            var course = await _mediator.Send(command);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course.Id);

        }
    }

}