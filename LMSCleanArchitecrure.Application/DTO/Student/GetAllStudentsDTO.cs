namespace LMSCleanArchitecrure.Application.DTO.Student
{
    public class GetAllStudentsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int RollNumber { get; set; }
        public string Degree { get; set; } = null!;
        public string Department { get; set; } = null!;
        //public string UserId { get; set; } = null!;
    }
}
