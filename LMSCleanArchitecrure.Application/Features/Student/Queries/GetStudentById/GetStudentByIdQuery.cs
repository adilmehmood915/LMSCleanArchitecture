using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IRequest<GetStudentByIdDTO>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
