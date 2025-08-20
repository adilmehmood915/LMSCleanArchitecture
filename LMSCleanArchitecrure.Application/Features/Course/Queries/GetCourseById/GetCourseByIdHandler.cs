using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Queries.GetCourseById
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, GetByIdDTO>
    {
        private readonly ICourseRepository courseRepository;
        public GetCourseByIdHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        async Task<GetByIdDTO> IRequestHandler<GetCourseByIdQuery, GetByIdDTO>.Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new ArgumentException("Invalid course ID.");
            var course = await courseRepository.GetCourseByIdAsync(request.Id);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found.");
            }
            return new GetByIdDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreditHours = course.CreditHours,
                Instructors = course.Instructors.Select(i => new InstructorResponseDTO
                {
                    Id = i.Id,
                    FullName = i.FullName,
                    Email = i.Email
                }).ToList()

            };
        }
    }
}
