using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetAllInstructor
{
    public class GetAllInstructorQuery : IRequest<List<GetAllInstructorDTO>>
    {

    }
}
