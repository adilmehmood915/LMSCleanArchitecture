using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById
{
    public class GetInstructorByIdQuery : IRequest<GetByIdInstructorDTO>
    {
        public int Id { get; }
        public GetInstructorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
