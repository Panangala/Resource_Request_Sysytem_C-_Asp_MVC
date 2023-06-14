using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assesment03.Models;

namespace Assesment03.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Assesment03.Models.Request> Request { get; set; }
        public DbSet<Assesment03.Models.Rsource> Rsource { get; set; }
    }
}