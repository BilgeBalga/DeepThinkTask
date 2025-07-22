// DeepThinkTask/Models/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace DeepThinkTask.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AboutUs>().HasData(
                new AboutUs { Id = 1, Content = "Şirketinizin tanıtım metni buraya gelecek...", EstablishmentYear = "2000", Mission = "Misyonunuz...", Vision = "Vizyonunuz...", ManagerName = "Yönetici Adı", ManagerImagePath = "" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}