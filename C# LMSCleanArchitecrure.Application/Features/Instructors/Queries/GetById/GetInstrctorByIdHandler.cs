using AutoMapper;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById
{
    internal class GetInstrctorByIdHandler : IRequestHandler<GetInstructorByIdQuery, GetByIdInstructorDTO>
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IMapper mapper;

        public GetInstrctorByIdHandler(IInstructorRepository instructorRepository, IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.mapper = mapper;
        }

        public async Task<GetByIdInstructorDTO> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0) throw new ArgumentException("Invalid instructor ID.");

            var entity = await instructorRepository.GetInstructorByIdAsync(request.Id);
            if (entity == null) return null!; 

            return mapper.Map<GetByIdInstructorDTO>(entity);
        }
    }
}
