using AutoMapper;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Command.UpdateInstructor
{
    internal class UpdateInstructorHandler : IRequestHandler<UpdateInstructorCommand, bool>
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IMapper mapper;

        public UpdateInstructorHandler(IInstructorRepository instructorRepository, IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = mapper.Map<Instructor>(request.InstructorDTO);
            instructor.Id = request.Id; // set BEFORE calling repository
            return await instructorRepository.UpdateInstructorAsync(instructor, cancellationToken);
        }
    }
}