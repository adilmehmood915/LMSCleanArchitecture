namespace LMSCleanArchitecrure.Application.DTO.Course
{
    public class CreateCourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreditHours { get; set; }
    }
}
