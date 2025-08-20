using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Command.AssignInstructor
{
    public class AssignInstructorToCourseCommand(int InstructorId, int CourseId) : IRequest<bool>
    {   public int InstructorId { get; set; } = InstructorId;
        public int CourseId { get; set; } = CourseId;
    }
}
