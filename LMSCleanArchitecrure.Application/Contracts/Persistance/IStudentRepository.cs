using LMSCleanArchitecture.Core.Entities;

namespace LMSCleanArchitecrure.Application.Contracts.Persistance
{
    public interface IStudentRepository
    {
        Task<IReadOnlyList<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<int> AddStudentAsync(Student student);
        Task<int> DeleteStudentAsync(int id);
        Task<bool> UpdateStudentAsync(Student student, CancellationToken cancellationToken);
        Task SaveChangesAsync();
        Task<bool> AssignCourseAsync(int studentId, int courseId, CancellationToken cancellationToken);

    }
}
