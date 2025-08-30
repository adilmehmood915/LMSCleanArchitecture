using LMSCleanArchitecrure.Application.DTO.Instructor;
using LMSCleanArchitecrure.Application.Features.Instructors.Command;
using LMSCleanArchitecrure.Application.Features.Instructors.Command.AssignInstructor;
using LMSCleanArchitecrure.Application.Features.Instructors.Command.DeleteInstructor;
using LMSCleanArchitecrure.Application.Features.Instructors.Command.UpdateInstructor;
using LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetAllInstructor;
using LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSCleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InstructorController : ControllerBase
    {
        private readonly IMediator mediator;

        public InstructorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var query = new GetAllInstructorQuery();
            var instructors = await mediator.Send(query);
            if (instructors == null || instructors.Count == 0)
            {
                return NotFound("No instructors found.");
            }
            return Ok(instructors);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstructor(CreateInstructorDTO instructorDTO)
        {
            var command = new CreateInstructorCommand(instructorDTO);
            var courseid = await mediator.Send(command);
            return Ok($"Instructor is added with Id {courseid}");
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin , Instructor")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var query = new GetInstructorByIdQuery(id);
            var instructor = await mediator.Send(query);
            if (instructor == null)
            {
                return NotFound($"Instructor with ID {id} not found.");
            }
            return Ok(instructor);
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInstructor(int id, UpdateInstructorDTO instructorDTO)
        {
            var command = new UpdateInstructorCommand(id, instructorDTO);
            var result = await mediator.Send(command);
            if (result)
            {
                return Ok("Instructor updated successfully.");
            }
            return BadRequest("Failed to update instructor. Please check the provided data.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            var command = new DeleteInstructorCommand(id);
            var result = await mediator.Send(command);
            return Ok($"Instructor with ID {result} deleted successfully.");
        }

        [Authorize(Roles = "Admin , Instructor")]
        [HttpPost("{courseId:int}/instructors/{instructorId:int}")]
        public async Task<IActionResult> AssignCourseToInstructorCommand(int courseId, int instructorId)
        {
            var ok = await mediator.Send(new AssignCourseToInstructorCommand(courseId, instructorId));
            return ok ? Ok() : NotFound();
        }
    }
}
