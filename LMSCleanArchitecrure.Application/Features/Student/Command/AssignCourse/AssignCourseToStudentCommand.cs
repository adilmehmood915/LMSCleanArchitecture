using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.AssignCourse
{
    public class AssignCourseToStudentCommand : IRequest<bool>
    {
        public int studentId { get; set; }
        public int courseId { get; set; }
        public AssignCourseToStudentCommand(int studentId, int courseId)
        {
            this.studentId = studentId;
            this.courseId = courseId;
        }
    }
}
