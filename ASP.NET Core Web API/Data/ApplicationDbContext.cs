using ASP.NET_Core_Web_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
            
        }
        public DbSet<Difficulty> Difficulty { get; set; }   
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }  
    }
}
