using LMSCleanArchitecrure.Application.DTO.Instructor;

namespace LMSCleanArchitecrure.Application.DTO.Course
{
    public class GetByIdDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreditHours { get; set; }

        public List<InstructorResponseDTO> Instructors { get; set; } = new();
    }
}
