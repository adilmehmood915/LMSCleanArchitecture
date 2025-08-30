using LMSCleanArchitecrure.Application.Contracts.Persistance;
using MediatR;

namespace LMSCleanArchitecrure.Application.Features.Student.Command.UpdateStudent
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentRepository studentRepository;
        public UpdateStudentHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.GetStudentByIdAsync(request.Id);
            if (student == null)
            {
                throw new KeyNotFoundException("Student with Id Does not exist.");
            }
            student.Name = request.UpdateDTO.Name;
            student.RollNumber = request.UpdateDTO.RollNumber;
            student.Degree = request.UpdateDTO.Degree;
            student.Department = request.UpdateDTO.Department;
            student.DateOfBirth = request.UpdateDTO.DateOfBirth;
            student.Address = request.UpdateDTO.Address;
            student.City = request.UpdateDTO.City;
            student.State = request.UpdateDTO.State;
            student.ZipCode = request.UpdateDTO.ZipCode;
            //student.UserId = student.UserId;

            var result = await studentRepository.UpdateStudentAsync(student, cancellationToken);
            if (!result)
            {
                throw new Exception("Student cannot be updated");
            }

            return true;
        }
    }
}
