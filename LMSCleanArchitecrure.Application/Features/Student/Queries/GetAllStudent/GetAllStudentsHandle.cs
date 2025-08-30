using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Queries.GetAllStudent
{
    public class GetAllStudentsHandle : IRequestHandler<GetAllStudentQuery, List<GetAllStudentsDTO>>
    {
        private readonly IStudentRepository studentRepository;
        public GetAllStudentsHandle(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        async Task<List<GetAllStudentsDTO>> IRequestHandler<GetAllStudentQuery, List<GetAllStudentsDTO>>.Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            var students = await studentRepository.GetAllStudentsAsync();
            var result = students.Select(c => new GetAllStudentsDTO
            {
                Id = c.Id,
                Name = c.Name,
                RollNumber = c.RollNumber,
                Degree = c.Degree,
                Department = c.Department,
                //UserId = c.UserId
            }).ToList();
            return result;
        }
    }
}
