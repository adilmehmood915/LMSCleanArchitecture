namespace LMSCleanArchitecture.Core.Entities
{
    public class Courses
    {
        public int Id { get; set; } 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreditHours { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    }
}
