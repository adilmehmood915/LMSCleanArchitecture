using LMSCleanArchitecture.Application.Contracts.Interfaces;
using LMSCleanArchitecture.Core.Entities;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly LMSDbContext context;

        public CourseRepository(LMSDbContext context)
        {
            this.context = context;
        }

        public async Task<Courses?> GetCourseByIdAsync(int id)
        {
            return await context.Courses.FindAsync(id);
        }

        public async Task<List<Courses>> GetAllCoursesAsync()
        {
            return await context.Courses.ToListAsync();
        }

        public async Task AddCourseAsync(Courses course)
        {
            await context.Courses.AddAsync(course);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await GetCourseByIdAsync(id);
            if (course == null)
                return false;

            context.Courses.Remove(course);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateCourseAsync(Courses course)
        {
            context.Courses.Update(course);
            await context.SaveChangesAsync();
        }


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        
        public async Task<bool> AssignInstructorToCourseAsync(int courseId, int instructorId, CancellationToken cancellationToken = default)
        {
            var course = await context.Courses
                .Include(c => c.Instructors)
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
            if (course == null)
            {
                return false;
            }
            var instructor = await context.Instructors
                .FirstOrDefaultAsync(i => i.Id == instructorId, cancellationToken);
            if (instructor == null)
            {
                return false;
            }
            course.Instructors.Add(instructor);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
