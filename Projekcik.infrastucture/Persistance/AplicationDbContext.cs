using Microsoft.EntityFrameworkCore;
using Projekcik.Entities;

namespace Projekcik.Infrastructure.Persistance
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Users> Zawodnicy { get; set; }
        public DbSet<Team> Druzyny { get; set; }
        public DbSet<Robots> Roboty { get; set; }
        public DbSet<Categories> Kategorie { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RobotyDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
