using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string StudentName { get; set; }

        [Required]
        public string Batch { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime LastModifiedOn { get; set; }

    }
}
