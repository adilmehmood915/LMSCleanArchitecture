using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;
namespace LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse
{
    public class CreateCourseCommand : IRequest<int>
    {
        public CourseResponseDTO CourseDto { get; }

        public CreateCourseCommand(CourseResponseDTO courseDto)
        {
            CourseDto = courseDto;
        }

        public CreateCourseCommand(CreateStudentDTO dto)
        {
        }
    }

}
