using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Command.UpdateCourse
{
    internal class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, CourseResponseDTO>
    {
        private readonly ICourseRepository courseRepository;
        public UpdateCourseHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<CourseResponseDTO> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseByIdAsync(request.Id);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {request.Id} not found.");
            }
            course.Title = request.CourseDTO.Title;
            course.Description = request.CourseDTO.Description;
            course.CreditHours = request.CourseDTO.CreditHours;
            await courseRepository.SaveChangesAsync();
            return new CourseResponseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreditHours = course.CreditHours
            };


        }
    }
}
