using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Student
    {
        public int Id { get; set; } 
        [Required]
        public string? Name { get; set; }
        [Required (ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9._]+\.[A-Za-z]{2,4}")]
        
        public string? Email { get; set; }
        [StringLength (100, MinimumLength =8)]
        [Required]
        public string? Password { get; set; }
        [Required]
        public Branch? Branch { get; set; }
        public Gender? Gender { get; set; }
        public bool IsRegular { get; set; }
        [DataType (DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }
        [Range(typeof(DateTime), "1/1/1963", "31/12/2005")]
        [DataType (DataType.Date)]
        [Required]
        public DateTime Date0fBorth { get; set; }

    }
}
