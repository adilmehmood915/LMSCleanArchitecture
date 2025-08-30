using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecture.Core.Entities;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using Microsoft.EntityFrameworkCore;


namespace LMSCleanArchitecrure.Infrastructure.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly LMSDbContext context;

        public InstructorRepository(LMSDbContext context)
        {
            this.context = context;
        }

        public async Task<Instructor> GetInstructorByIdAsync(int id)
        {
            var instructor = await context.Instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (instructor == null)
            {
                throw new InvalidOperationException("Instructor not found.");
            }
            return instructor;
        }

        public async Task<IReadOnlyList<Instructor>> GetAllInstructorAsync()
        {
            return await context.Instructors.ToListAsync();
        }

        public async Task<Instructor> DeleteInstructorAsync(int id)
        {
            var instructor = await GetInstructorByIdAsync(id);
            if (instructor != null)
            {
                context.Instructors.Remove(instructor);
                return instructor;
            }
            else
            {
                throw new InvalidOperationException("Instructor not found.");
            }
        }

        public async Task<bool> UpdateInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
        {
            var existingInstructor = await context.Instructors.FindAsync(new object[] { instructor.Id }, cancellationToken);
            if (existingInstructor != null)
            {
                existingInstructor.FullName = instructor.FullName;
                existingInstructor.Email = instructor.Email;
                existingInstructor.Courses = instructor.Courses;
                context.Instructors.Update(existingInstructor);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> AddInstructorAsync(Instructor instructor)
        {

            context.Instructors.Add(instructor);
            return instructor.Id;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> AssignCourseToInstructorAsync(int instructorId, int courseId, CancellationToken cancellationToken)
        {
            var instructor = await context.Instructors
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == instructorId, cancellationToken);
            if (instructor == null)
            {
                throw new InvalidOperationException("Instructor not found.");
            }
            var course = await context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
            if (course == null)
            {
                throw new InvalidOperationException("Course not found.");
            }
            instructor.Courses.Add(course);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
