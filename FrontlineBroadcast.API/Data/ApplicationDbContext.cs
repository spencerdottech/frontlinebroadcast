using Microsoft.EntityFrameworkCore;
using FrontlineBroadcast.API.Models;

namespace FrontlineBroadcast.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Add some seed data
            modelBuilder.Entity<Notification>().HasData(
                new Notification 
                { 
                    Id = 1, 
                    Title = "System Maintenance", 
                    Message = "The system will be down for maintenance tonight from 10PM to 12AM.", 
                    CreatedAt = DateTime.UtcNow
                },
                new Notification 
                { 
                    Id = 2, 
                    Title = "Staff Meeting", 
                    Message = "Reminder: All staff meeting tomorrow at 9AM in the main conference room.",
                    CreatedAt = DateTime.UtcNow.AddHours(-24)
                }
            );
        }
    }
}