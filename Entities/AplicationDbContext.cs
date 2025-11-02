using Microsoft.EntityFrameworkCore;

namespace Projekcik.Entities
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Users> Zadownicy { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RobotyDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
