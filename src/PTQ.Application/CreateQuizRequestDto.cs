using System.ComponentModel.DataAnnotations;

namespace PTQ.Application
{
    public class CreateQuizRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string PotatoTeacherName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Path { get; set; }
    }
}