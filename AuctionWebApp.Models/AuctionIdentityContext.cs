using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionWebApp.Models
{
    public class AuctionIdentityContext : IdentityDbContext<User>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Bet> Bets { get; set; }

        public AuctionIdentityContext(DbContextOptions<AuctionIdentityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Fixture>()
        //        .HasOne(f => f.HomeTeam)
        //        .WithMany(t => t.HomeFixtures)
        //        .HasForeignKey(t => t.HomeTeamId)
        //        .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Fixture>()
        //        .HasOne(f => f.AwayTeam)
        //        .WithMany(t => t.AwayFixtures)
        //        .HasForeignKey(t => t.AwayTeamId)
        //        .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
        //}

    }
}