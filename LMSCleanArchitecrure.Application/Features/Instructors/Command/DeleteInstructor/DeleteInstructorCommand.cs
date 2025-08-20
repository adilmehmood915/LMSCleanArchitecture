using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.DeleteInstructor
{
    public class DeleteInstructorCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteInstructorCommand(int id)
        {
            Id = id;
        }
    }
}
