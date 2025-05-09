using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTQ.Models
{
    [Table("PotatoTeacher")]
    public class PotatoTeacher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; } = new HashSet<Quiz>();
    }
}