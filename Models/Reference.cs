using System.ComponentModel.DataAnnotations;

namespace DeepThinkTask.Models 
{
    public class Reference
    {
        public int Id { get; set; } 
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } 
        [MaxLength(250)]
        public string LogoPath { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public int Order { get; set; }
    }
}