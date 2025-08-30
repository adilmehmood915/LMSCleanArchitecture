using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public UpdateStudentDTO UpdateDTO { get; set; }

        public UpdateStudentCommand(int id, UpdateStudentDTO updateDTO)
        {
            Id = id;
            UpdateDTO = updateDTO;
        }

    }
}
