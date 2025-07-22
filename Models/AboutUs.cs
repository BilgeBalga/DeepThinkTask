// DeepThinkTask/Models/AboutUs.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // [NotMapped] için
using Microsoft.AspNetCore.Http; // IFormFile için gerekli

namespace DeepThinkTask.Models
{
    public class AboutUs
    {
        public int Id { get; set; } = 1;
        [Required]
        public required string Content { get; set; }
        [MaxLength(50)]
        public required string EstablishmentYear { get; set; }
        [MaxLength(1000)]
        public required string Mission { get; set; }
        [MaxLength(1000)]
        public required string Vision { get; set; }
        [MaxLength(100)]
        public required string ManagerName { get; set; }
        [MaxLength(250)]
        public required string ManagerImagePath { get; set; } 

        [NotMapped]
        public IFormFile? ManagerImageFile { get; set; } 
    }
}