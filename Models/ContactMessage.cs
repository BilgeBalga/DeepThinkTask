using System.ComponentModel.DataAnnotations;

namespace DeepThinkTask.Models
{
    public class ContactMessage
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } 
        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } 
        [Required]
        [MaxLength(500)]
        public string Message { get; set; } 
        public DateTime SentDate { get; set; } = DateTime.Now; 
        public bool IsRead { get; set; } = false; 
    }
}