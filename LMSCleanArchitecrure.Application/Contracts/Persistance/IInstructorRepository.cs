using LMSCleanArchitecture.Core.Entities;
using System.Threading.Tasks;

namespace LMSCleanArchitecrure.Application.Contracts.Persistance
{
    public interface IInstructorRepository
    {
        public Task<Instructor> GetInstructorByIdAsync(int id);
        public Task<IReadOnlyList<Instructor>> GetAllInstructorAsync();
        Task<int> AddInstructorAsync(Instructor instructor);
        Task<Instructor> DeleteInstructorAsync(int id);
        Task<bool> UpdateInstructorAsync(Instructor instructor, CancellationToken cancellationToken);
        Task SaveChangesAsync();
        public Task<bool> AssignCourseToInstructorAsync(int instructorId, int courseId, CancellationToken cancellationToken);

    }
}
