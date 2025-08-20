using LMSCleanArchitecrure.Application.Contracts.Persistance;
using LMSCleanArchitecture.Core.Entities;
using LMSCleanArchitecture.Infrastructure.Persisitense;
using Microsoft.EntityFrameworkCore;

namespace LMSCleanArchitecture.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LMSDbContext context;
        public StudentRepository(LMSDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> AddStudentAsync(Student student)
        {
            context.Add(student);
            await context.SaveChangesAsync();
            return student.Id;
        }

        public async Task<Student> DeleteStudentAsync(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return student;

        }

        public async Task<IReadOnlyList<Student>> GetAllStudentsAsync()
        {
            return await context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");

            return student;
        }


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            var students = await context.Students.FindAsync(student.Id);
            if (students == null)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} not found.");
            }
            students.Name = student.Name;
            students.RollNumber = student.RollNumber;
            students.Courses = student.Courses;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
