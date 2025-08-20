using MediatR;
using LMSCleanArchitecture.Core.Entities;
using LMSCleanArchitecrure.Application.DTO.Course;

namespace LMSCleanArchitecrure.Application.Features.Course.Queries.GetAllCourses
{
    public class GetAllCoursesQuery : IRequest<List<GetAllCourseDTO>>
    {
    }
}
