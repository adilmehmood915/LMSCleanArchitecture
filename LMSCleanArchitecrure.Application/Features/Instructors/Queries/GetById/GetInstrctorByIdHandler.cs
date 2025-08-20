using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.DTO.Instructor;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Instructors.Queries.GetById
{
    internal class GetInstrctorByIdHandler : IRequestHandler<GetInstructorByIdQuery, GetByIdInstructorDTO>
    {
        private readonly IInstructorRepository instructorRepository;

        public GetInstrctorByIdHandler(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }

        public async Task<GetByIdInstructorDTO> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new ArgumentException("Instructor ID must be greater than zero.", nameof(request.Id));

            var instructor = await instructorRepository.GetInstructorByIdAsync(request.Id);
            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID {request.Id} not found.");
            return new GetByIdInstructorDTO
            {
                Id = instructor.Id,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Courses = instructor.Courses.Select(c => new CourseResponseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreditHours = c.CreditHours
                }).ToList()
            };
        }
    }
}
