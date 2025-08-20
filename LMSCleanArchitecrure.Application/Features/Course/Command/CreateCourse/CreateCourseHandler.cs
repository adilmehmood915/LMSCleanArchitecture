using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.Features.Command.Course.CreateCourse;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecture.Application.Features.Course.CreateCourse
{
    internal class CreateCourseHandler : IRequestHandler<CreateCourseCommand, int>
    {
        private readonly ICourseRepository courseRepository;

        public CreateCourseHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.CourseDto.CreditHours < 1)
                throw new ArgumentException("Credit hours must be at least 1.");

            var course = new Courses
            {
                Title = request.CourseDto.Title,
                Description = request.CourseDto.Description,
                CreditHours = request.CourseDto.CreditHours
            };

            await courseRepository.AddCourseAsync(course);
            return course.Id;
        }
    }
}
