using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.DeleteStudent
{
    public class DeleteStudentHandle : IRequestHandler<DeleteStudentCommand, int>
    {
        private readonly IStudentRepository studentRepository;

        public DeleteStudentHandle(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<int> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // Call the repository to delete the student
            return await studentRepository.DeleteStudentAsync(request.Id);
        }
    }
}
