using Microsoft.EntityFrameworkCore;
using ContactUs.Models;




namespace ContactUs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ContactEmail> ContactEmails { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}