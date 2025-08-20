using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using LMSCleanArchitecrure.Application.Features.Course.Command.AssignInstructor;
using LMSCleanArchitecrure.Application.Features.Course.Command.DeleteCourse;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetCourseById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LMSCleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator mediator;
        public CourseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseResponseDTO dto)
        {
            var command = new CreateCourseCommand(dto); 
            var courseId = await mediator.Send(command);
            return Ok(new { id = courseId });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var result = await mediator.Send(new GetAllCoursesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseByIdAsync(int id)
        {
            var result = await mediator.Send(new GetCourseByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDTO dto)
        {
            var command = new UpdateCourseCommand(id, dto);
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseAsync(int id)
        {
            var command = new DeleteCourseCommand(id);
            var result = await mediator.Send(command);
            if (!result)
            {
                return NotFound($"Course with ID {id} not found.");
            }
            return NoContent();
        }
        [HttpPost("{courseId:int}/instructors/{instructorId:int}")]
        public async Task<IActionResult> AssignInstructorToCourse(int courseId, int instructorId)
        {
            var ok = await mediator.Send(new AssignInstructorToCourseCommand(courseId, instructorId));
            return ok ? Ok() : NotFound();
        }
    }
}
