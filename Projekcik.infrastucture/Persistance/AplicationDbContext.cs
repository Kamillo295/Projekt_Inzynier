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
        public DbSet<Games> Gry { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Games>()
                .HasOne(g => g.Robot1)
                .WithMany()
                .HasForeignKey(g => g.Robot1ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Games>()
                .HasOne(g => g.Robot2)
                .WithMany()
                .HasForeignKey(g => g.Robot2ID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
