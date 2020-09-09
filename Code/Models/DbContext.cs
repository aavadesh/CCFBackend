using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class _DbContext : DbContext
    {
        public _DbContext(DbContextOptions<_DbContext> options) : base(options)
        {

        }


        public DbSet<CompetencyFramework> CompetencyFramework { get; set; }

        public DbSet<CompetencyDetail> CompetencyDetail { get; set; }
        public DbSet<Information> Information { get; set; }
    }
}
