using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Course;
using LMSCleanArchitecrure.Application.DTO.Student;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LMSCleanArchitecrure.Application.Features.Student.Queries.GetStudentById
{
    internal class GetStudentByIdHandle : IRequestHandler<GetStudentByIdQuery, GetStudentByIdDTO>
    {
        private readonly IStudentRepository studentRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GetStudentByIdHandle(IStudentRepository studentRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.studentRepository = studentRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetStudentByIdDTO> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the logged-in user's UserId from the claims
            var loggedInUserId = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            // Retrieve the student by the provided Id
            var student = await studentRepository.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with Id {request.Id} not found.");
            }

            // Validate that the logged-in user's UserId matches the student's UserId
            if (student.UserId != loggedInUserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to access this student's information.");
            }

            // Map the student entity to the DTO and return it
            var result = new GetStudentByIdDTO
            {
                Name = student.Name,
                RollNumber = student.RollNumber,
                Degree = student.Degree,
                Department = student.Department,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                City = student.City,
                State = student.State,
                ZipCode = student.ZipCode,
                Courses = student.StudentCourses
                    .Select(sc => sc.Course)
                    .Select(c => new CourseResponseDTO
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Description = c.Description,
                        CreditHours = c.CreditHours
                    }).ToList()
            };

            return result;
        }
    }
}