using AutoMapper;
using LMSCleanArchitecrure.Application.Features.Instructors.Command.AssignInstructor;
using LMSCleanArchitecture.Application.Contracts.Interfaces;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Course.AssignInstructor
{
    internal class AssignInstructorToCourseHandler
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        public AssignInstructorToCourseHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(AssignCourseToInstructorCommand request , CancellationToken cancellationToken)
        {
            var ok = await courseRepository.AssignInstructorToCourseAsync(request.InstructorId, request.CourseId, cancellationToken);
            if (!ok)
            {
                throw new KeyNotFoundException("Course or Instructor not found.");
            }
            return true;


        }
    }
}
