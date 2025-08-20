using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecrure.Application.DTO.Student;
using LMSCleanArchitecture.Core.Entities;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.CreateStudent
{
    internal class CreateStudentHandle : IRequestHandler<CreateStudentCommand, int>
    {
        private readonly IStudentRepository studentRepository;
        public CreateStudentHandle(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var students = new LMSCleanArchitecture.Core.Entities.Student
            {
                Name = request.StudentDTO.Name,
                RollNumber = request.StudentDTO.RollNumber,
                Degree = request.StudentDTO.Degree,
                Department = request.StudentDTO.Department,
                DateOfBirth = request.StudentDTO.DateOfBirth,
                Address = request.StudentDTO.Address,
                City = request.StudentDTO.City,
                State = request.StudentDTO.State,
                ZipCode = request.StudentDTO.ZipCode
            };
            if (students.RollNumber < 1)
                throw new ArgumentException("Roll number must be at least 1.");
            var result = await studentRepository.AddStudentAsync(students);
            if (result == 0)
            {
                throw new Exception("Failed to create student.");
            }
            return students.Id;
        }
    }
}
