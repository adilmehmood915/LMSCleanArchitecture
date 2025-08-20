using LMSCleanArchitecrure.Application.DTO.Course;
using MediatR;

public class UpdateCourseCommand : IRequest<CourseResponseDTO>

{
    public int Id { get; }
    public UpdateCourseDTO CourseDTO { get; }

    public UpdateCourseCommand(int id, UpdateCourseDTO courseDto)
    {
        Id = id;
        CourseDTO = courseDto;
    }
}
