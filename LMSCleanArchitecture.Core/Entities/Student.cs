namespace LMSCleanArchitecture.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RollNumber { get; set; }
        public string Degree { get; set; } = null!;
        public string Department { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string? UserId { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
