using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SweetAndSavory.Models
{
    public class SweetAndSavoryContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Treat> Treats { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<FlavorTreat> FlavorTreats { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public SweetAndSavoryContext(DbContextOptions options) : base(options) { }
    }
   
 } 