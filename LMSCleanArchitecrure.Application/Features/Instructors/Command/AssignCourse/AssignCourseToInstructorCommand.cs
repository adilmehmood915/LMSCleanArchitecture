using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.AssignInstructor
{
    public record AssignCourseToInstructorCommand(int CourseId, int InstructorId) : IRequest<bool>;
}