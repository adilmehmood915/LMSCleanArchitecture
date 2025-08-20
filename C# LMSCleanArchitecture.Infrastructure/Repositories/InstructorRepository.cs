using LMSCleanArchitecture.Core.Entities;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using LMSCleanArchitecrure.Application.Contracts.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly LMSDbContext context;
        public InstructorRepository(LMSDbContext context) => this.context = context;

        public async Task<Instructor> GetInstructorByIdAsync(int id)
            => await context.Instructors.FindAsync(id);

        public async Task<IReadOnlyList<Instructor>> GetAllInstructorAsync()
            => await context.Instructors.ToListAsync();

        public async Task<int> AddInstructorAsync(Instructor instructor)
        {
            await context.Instructors.AddAsync(instructor);
            await context.SaveChangesAsync();              
            return instructor.Id;
        }

        public async Task<Instructor> DeleteInstructorAsync(int id)
        {
            var instructor = await GetInstructorByIdAsync(id);
            if (instructor == null) throw new InvalidOperationException("Instructor not found.");
            context.Instructors.Remove(instructor);
            await context.SaveChangesAsync();
            return instructor;
        }

        public async Task<bool> UpdateInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
        {
            var existing = await context.Instructors.FindAsync(new object[] { instructor.Id }, cancellationToken);
            if (existing == null) return false;

            existing.FullName = instructor.FullName;
            existing.Email = instructor.Email;
            existing.Courses = instructor.Courses;

            await context.SaveChangesAsync(cancellationToken); 
            return true;
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}