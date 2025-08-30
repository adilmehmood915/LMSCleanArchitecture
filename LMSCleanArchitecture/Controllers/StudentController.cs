using LMSCleanArchitecrure.Application.DTO.Student;
using LMSCleanArchitecrure.Application.Features.Student.Command.AssignCourse;
using LMSCleanArchitecrure.Application.Features.Student.Command.CreateStudent;
using LMSCleanArchitecrure.Application.Features.Student.Command.DeleteStudent;
using LMSCleanArchitecrure.Application.Features.Student.Command.UpdateStudent;
using LMSCleanArchitecrure.Application.Features.Student.Queries.GetAllStudent;
using LMSCleanArchitecrure.Application.Features.Student.Queries.GetStudentById;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSCleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentController(IMediator mediator, LMSDbContext db)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO dto)
        {
            var command = new CreateStudentCommand(dto);
            var studentId = await mediator.Send(command);
            return Ok($"Student is added with Id {studentId}");
        }
        [HttpPost("{id}")]
        [Authorize (Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDTO dto)
        {
            var command = new UpdateStudentCommand(id, dto);
            var result = await mediator.Send(command);
            return result ? Ok(true) : NotFound(false);
        }

        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var result = await mediator.Send(new DeleteStudentCommand(id));
                return Ok($"Student with ID {result} deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var query = new GetAllStudentQuery();
            var result = await mediator.Send(query);
            if (result == null)
            {
                return NotFound("Students not found.");
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize (Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = (await mediator.Send(query));
            if (result == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("{studentId}/courses/{courseId}")]
        [Authorize (Roles = "Admin,Student")]
        public async Task<IActionResult> AssignCourseToStudent([FromRoute] int studentId, [FromRoute] int courseId)
        {
            var command = new AssignCourseToStudentCommand(studentId, courseId);
            var ok = await mediator.Send(command);
            return Ok(ok);
        }
    }
}
