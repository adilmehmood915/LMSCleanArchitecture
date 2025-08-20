using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetAllInstructor
{
    internal class GetAllInstructorHandle : IRequestHandler<GetAllInstructorQuery, List<GetAllInstructorDTO>>
    {
        private readonly IInstructorRepository instructorRepository;
        public GetAllInstructorHandle(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }
        public async Task<List<GetAllInstructorDTO>> Handle(GetAllInstructorQuery request, CancellationToken cancellationToken)
        {
            var instructors = await instructorRepository.GetAllInstructorAsync();
            return instructors.Select(c => new GetAllInstructorDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
            }).ToList();
        }
    }
}
