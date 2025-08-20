using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.AssignInstructor
{
    internal class AssignCourseToInstructorHandler : IRequestHandler<AssignCourseToInstructorCommand, bool>
    {
        private readonly IInstructorRepository instructorRepository;

        public AssignCourseToInstructorHandler(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }
        
        public async Task<bool> Handle(AssignCourseToInstructorCommand request, CancellationToken cancellationToken)
        {
            var ok = await instructorRepository.AssignCourseToInstructorAsync(request.InstructorId, request.CourseId, cancellationToken);
            if (!ok) throw new KeyNotFoundException("Course or Instructor not found or assignment failed.");
            return true;
        }
    }
}