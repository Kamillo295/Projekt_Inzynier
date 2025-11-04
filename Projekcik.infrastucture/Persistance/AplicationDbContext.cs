using Microsoft.EntityFrameworkCore;
using Projekcik.Entities;

namespace Projekcik.Infrastructure.Persistance
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext>options):base(options)
        {

        }
        public DbSet<Users> Zawodnicy { get; set; }
        public DbSet<Team> Druzyny { get; set; }
        public DbSet<Robots> Roboty { get; set; }
        public DbSet<Categories> Kategorie { get; set; }
    }
}
