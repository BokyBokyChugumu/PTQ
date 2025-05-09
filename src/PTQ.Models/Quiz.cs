using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTQ.Models
{
    [Table("Quiz")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int PotatoTeacherId { get; set; }

        [ForeignKey("PotatoTeacherId")]
        public virtual PotatoTeacher PotatoTeacher { get; set; }

        [Required]
        [MaxLength(255)]
        public string PathFile { get; set; }
    }
}