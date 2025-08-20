using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command
{
    public class CreateInstructorCommand : IRequest<int>
    {
        public CreateInstructorDTO InstructorDTO { get; }

        public CreateInstructorCommand(CreateInstructorDTO instructorDTO)
        {
            this.InstructorDTO = instructorDTO;
        }
    }
}
