using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.CreateStudent
{
    public class CreateStudentCommand : IRequest<int>
    {
        public CreateStudentDTO StudentDTO { get; }
        public CreateStudentCommand(CreateStudentDTO studentDto)
        {
            StudentDTO = studentDto;
        }
    }
}
