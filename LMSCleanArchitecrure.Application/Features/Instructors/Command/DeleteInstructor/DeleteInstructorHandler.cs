using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.DeleteInstructor
{
    internal class DeleteInstructorHandler : IRequestHandler<DeleteInstructorCommand, int>
    {
        private readonly IInstructorRepository _instructorRepository;

        public DeleteInstructorHandler(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public async Task<int> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {

            var result = await _instructorRepository.DeleteInstructorAsync(request.Id);
            if (result == null)
            {
                throw new Exception($"Failed to delete instructor with ID {request.Id}.");
            }


            await _instructorRepository.SaveChangesAsync();
            return request.Id;
        }
    }
}
