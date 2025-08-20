using LMSCleanArchitecture.Core.Entities;

namespace LMSCleanArchitecture.Application.Contracts.Interfaces
{
    public interface ICourseRepository
    {
        Task<Courses?> GetCourseByIdAsync(int id);
        Task<List<Courses>> GetAllCoursesAsync();
        Task AddCourseAsync(Courses course);
        Task<bool> DeleteCourseAsync(int id);
        Task UpdateCourseAsync(Courses course);
        Task SaveChangesAsync();
        Task<bool> AssignInstructorToCourseAsync(int courseId, int instructorId, CancellationToken cancellationToken);
    }
}
