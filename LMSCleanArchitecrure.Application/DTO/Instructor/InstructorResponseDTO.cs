using LMSCleanArchitecrure.Application.DTO.Course;

namespace LMSCleanArchitecrure.Application.DTO.Instructor
{
    public class InstructorResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
