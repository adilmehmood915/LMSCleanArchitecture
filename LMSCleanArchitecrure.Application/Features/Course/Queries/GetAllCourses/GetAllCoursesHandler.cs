using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses;
using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, List<GetAllCourseDTO>>
    {
        private readonly ICourseRepository courseRepository;
        public GetAllCoursesHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public async Task<List<GetAllCourseDTO>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await courseRepository.GetAllCoursesAsync();
            return courses.Select(c => new GetAllCourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreditHours = c.CreditHours
            }).ToList();
        }
    }
}
