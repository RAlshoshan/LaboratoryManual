using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LabM.Models;

namespace LabM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LabM.Models.Request>? Request { get; set; }
        public DbSet<LabM.Models.Management>? Management { get; set; }
        public DbSet<LabM.Models.College>? College { get; set; }
    }
}