namespace StudentManagement.Api.Model
{
    public class StudentDetailsResponseDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Batch { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }

    }
}
