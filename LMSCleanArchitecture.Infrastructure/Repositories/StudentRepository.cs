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

        public async Task<int> DeleteStudentAsync(int id)
        {
            var student = await context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            }
            if (student.StudentCourses != null && student.StudentCourses.Any())
            {
                context.StudentCourse.RemoveRange(student.StudentCourses);
            }

            // Remove the student
            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return student.Id;
        }
        public async Task<IReadOnlyList<Student>> GetAllStudentsAsync()
        {
            return await context.Students
                .Select(s => new Student
                {
                    Id = s.Id,
                    Name = s.Name ?? string.Empty,
                    RollNumber = s.RollNumber,
                    Degree = s.Degree ?? string.Empty,
                    Department = s.Department ?? string.Empty,
                    DateOfBirth = s.DateOfBirth,
                    Address = s.Address ?? string.Empty,
                    City = s.City ?? string.Empty,
                    State = s.State ?? string.Empty,
                    ZipCode = s.ZipCode ?? string.Empty,
                    //UserId = s.UserId ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await context.Students
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");

            return student;
        }

        public async Task SaveChangesAsync()
        {
             await context.SaveChangesAsync();
        }

   

        public async Task<bool> AssignCourseAsync(int studentId, int courseId, CancellationToken cancellationToken)
        {
            var studentExists = await context.Students.AnyAsync(s => s.Id == studentId, cancellationToken);
            if (!studentExists)
                throw new KeyNotFoundException($"Student with ID {studentId} not found.");

            var courseExists = await context.Courses.AnyAsync(c => c.Id == courseId, cancellationToken);
            if (!courseExists)
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");

            var alreadyAssigned = await context.Set<StudentCourse>()
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId, cancellationToken);

            if (alreadyAssigned)
                return false;
            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            await context.Set<StudentCourse>().AddAsync(studentCourse, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<bool> UpdateStudentAsync(Student student, CancellationToken cancellationToken)
        {
            var existingStudent = await context.Students.FindAsync(new object[] { student.Id }, cancellationToken);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException($"Student with ID {student.Id} not found.");
            }

            existingStudent.Name = student.Name;
            existingStudent.RollNumber = student.RollNumber;
            existingStudent.Degree = student.Degree;
            existingStudent.Department = student.Department;
            existingStudent.DateOfBirth = student.DateOfBirth;
            existingStudent.Address = student.Address;
            existingStudent.City = student.City;
            existingStudent.State = student.State;
            existingStudent.ZipCode = student.ZipCode;
            existingStudent.UserId = student.UserId;

            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}