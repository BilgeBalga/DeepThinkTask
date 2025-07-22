using System.ComponentModel.DataAnnotations;

namespace DeepThinkTask.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [MaxLength(500)]
        public required string Description { get; set; } 
                                                        
        [Required]
        public required string ImagePath { get; set; } 
        public int Order { get; set; }
    }
}