using LMSCleanArchitecture.Application.Contracts.Interfaces;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Command.DeleteCourse
{
    internal class DeleteCourseHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseRepository courseRepository;

        public DeleteCourseHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var deleted = await courseRepository.DeleteCourseAsync(request.CourseId); 

            if (deleted)
            {
                await courseRepository.SaveChangesAsync();
            }
            return deleted;
        }


    }
}
