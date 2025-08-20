using AutoMapper;
using LMSCleanArchitecrure.Application.DTO.Student;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSCleanArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public IMediator mediator { get; }
        public StudentController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDTO dto, IMapper mapper)
        {
            var command = new CreateCourseCommand(dto);
            var studentId = await mediator.Send(command);
            return Ok($"Student is added with Id {studentId}");

        }
    }
}
