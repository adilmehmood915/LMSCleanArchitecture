using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.UpdateInstructor
{
    internal class UpdateInstructorHandler : IRequestHandler<UpdateInstructorCommand, bool>
    {
        private readonly IInstructorRepository instructorRepository;

        public UpdateInstructorHandler(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }

        public async Task<bool> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await instructorRepository.GetInstructorByIdAsync(request.Id);
            if (instructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {request.Id} not found.");
            }

            var updatedInstructor = new Instructor
            {
                Id = request.Id,
                FullName = request.InstructorDTO.FullName,
                Email = request.InstructorDTO.Email,
            };

            var result = await instructorRepository.UpdateInstructorAsync(updatedInstructor, cancellationToken);
            if (!result)
            {
                throw new Exception($"Failed to update instructor with ID {request.Id}.");
            }
            await instructorRepository.SaveChangesAsync();
            return true;
        }
    }
}
