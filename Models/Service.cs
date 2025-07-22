using System.ComponentModel.DataAnnotations;

namespace DeepThinkTask.Models 
{
    public class Service
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } 
        [MaxLength(1000)]
        public string Description { get; set; } 
        [MaxLength(250)]
        public string Icon { get; set; } 
        [MaxLength(250)]
        public string ImagePath { get; set; }
        public int Order { get; set; } 
    }
}