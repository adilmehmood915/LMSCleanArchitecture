using AutoMapper;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;
using LMSCleanArchitecture.Core.Entities; 
namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.CreateInstructor
{
    internal class CreateInstructorHandler : IRequestHandler<CreateInstructorCommand, int>
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IMapper mapper;

        public CreateInstructorHandler(IInstructorRepository instructorRepository, IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = mapper.Map<Instructor>(request.InstructorDTO);
            var id = await instructorRepository.AddInstructorAsync(instructor);
            return id;
        }
    }
}