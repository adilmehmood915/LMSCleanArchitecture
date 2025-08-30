using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;
namespace LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse
{
    public class CreateCourseCommand : IRequest<int>
    {
        public CreateCourseDTO CourseDto { get; }

        public CreateCourseCommand(CreateCourseDTO courseDto)
        {
            CourseDto = courseDto;
        }
    }

}
