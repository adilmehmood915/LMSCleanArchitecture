using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Queries.GetAllStudent
{
    public class GetAllStudentQuery : IRequest<List<GetAllStudentsDTO>>
    {
    }
}
