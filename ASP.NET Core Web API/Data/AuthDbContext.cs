using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // seeding roles 
            base.OnModelCreating(builder);
            var readRoleId = "03654361-6b0d-41d4-aa15-21dacf6bcfd1";
            var writeRoleId = "935bb9fe-2c05-4ce1-a6b1-67a0f1c6f177";

            var roles = new List<IdentityRole> {
                new IdentityRole
                {
                    Id = readRoleId,
                    ConcurrencyStamp = readRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
                };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
