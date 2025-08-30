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
            if (request.Id <= 0)
                throw new ArgumentException("Instructor Id must be provided and greater than zero.", nameof(request.Id));

            // Use correct repository method and parameter type
            var instructor = await instructorRepository.GetInstructorByIdAsync(request.Id);
            if (instructor == null)
            {
                throw new KeyNotFoundException($"Instructor with Id {request.Id} not found.");
            }

            instructor.FullName = request.InstructorDTO.FullName;
            instructor.Email = request.InstructorDTO.Email;

            var result = await instructorRepository.UpdateInstructorAsync(instructor, cancellationToken);
            if (!result)
            {
                throw new Exception($"Failed to update instructor with Id {request.Id}.");
            }
            await instructorRepository.SaveChangesAsync();
            return true;
        }
    }
}
