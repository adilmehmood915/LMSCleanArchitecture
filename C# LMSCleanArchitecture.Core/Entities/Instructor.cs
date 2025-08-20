namespace LMSCleanArchitecture.Core.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<string> Courses { get; set; } = new(); // matches value converter
    }
}