using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Command.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<bool>
    {   public int CourseId { get; set; }
        public DeleteCourseCommand(int courseId)
        {
            CourseId = courseId;
        }
    }
}
