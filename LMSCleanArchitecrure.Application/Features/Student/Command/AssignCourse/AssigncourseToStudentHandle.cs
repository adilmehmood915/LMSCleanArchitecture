using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.AssignCourse
{
    internal class AssigncourseToStudentHandle : IRequestHandler<AssignCourseToStudentCommand, bool>
    {
        private readonly IStudentRepository _studentRepository;

        public AssigncourseToStudentHandle(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<bool> Handle(AssignCourseToStudentCommand request, CancellationToken cancellationToken)
        {
            return await _studentRepository.AssignCourseAsync(request.studentId, request.courseId, cancellationToken);
        }
    }
}
