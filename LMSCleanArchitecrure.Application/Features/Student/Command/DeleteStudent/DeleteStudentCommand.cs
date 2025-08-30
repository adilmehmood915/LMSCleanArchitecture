using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.DeleteStudent
{
    public class DeleteStudentCommand : IRequest<int>
    {
        public int Id { get; }

        public DeleteStudentCommand(int id)
        {
            Id = id;
        }
    }
}
