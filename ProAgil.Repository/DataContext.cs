using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Events { get; set;}

        public DbSet<Guest> Guests { get; set; }

        public DbSet<SocialNetwork> SocialNetWorks { get; set;}

        public DbSet<Lot> Lots { get; set; }

        public DbSet<GuestEvent> GuestsEvents { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // specifies keys for n -> n relation
            modelBuilder.Entity<GuestEvent>().HasKey(x => new { x.EventId, x.GuestId});
        }
    }
}