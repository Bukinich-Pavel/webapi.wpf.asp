using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task22WebApi.Models
{
    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<PhoneBook> PhoneBooks { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PhoneBook1;Trusted_Connection=True;");
        }
    }
}
