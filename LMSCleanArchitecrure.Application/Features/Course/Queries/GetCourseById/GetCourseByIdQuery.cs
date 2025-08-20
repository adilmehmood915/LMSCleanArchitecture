using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Course.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<GetByIdDTO>
    {
        public int Id { get; set; }

        
        public GetCourseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
