using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;
using LMSCleanArchitecture.Core.Entities;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.CreateInstructor
{
    internal class CreateInstructorHandler : IRequestHandler<CreateInstructorCommand, int>
    {
        private readonly IInstructorRepository instructorRepository;

        public CreateInstructorHandler(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }

        public async Task<int> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = new Instructor
            {
                FullName = request.InstructorDTO.FullName,
                Email = request.InstructorDTO.Email,
            };
            await instructorRepository.AddInstructorAsync(instructor);
            await instructorRepository.SaveChangesAsync();
            return instructor.Id;
        }
    }
}

