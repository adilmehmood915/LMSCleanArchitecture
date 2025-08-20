using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.UpdateInstructor
{
    public class UpdateInstructorCommand : IRequest<bool>
    {
        public UpdateInstructorDTO InstructorDTO { get; }
        public int Id { get; }

        public UpdateInstructorCommand(int id , UpdateInstructorDTO InstructorDTO )
        {
            this.InstructorDTO = InstructorDTO;
            Id = id;
        }
        
    }
}
