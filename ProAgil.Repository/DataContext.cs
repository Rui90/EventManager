using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;
using System;
using Microsoft.AspNetCore.Identity;

namespace ProAgil.Repository.Data
{
    public class DataContext : IdentityDbContext<User, Role, int,
                                                IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Events { get; set;}

        public DbSet<Guest> Guests { get; set; }

        public DbSet<SocialNetwork> SocialNetWorks { get; set;}

        public DbSet<Lot> Lots { get; set; }

        public DbSet<GuestEvent> GuestsEvents { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            // specifies keys for n -> n relation

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});
                    userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();
                    userRole.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
                });
            modelBuilder.Entity<GuestEvent>().HasKey(x => new { x.EventId, x.GuestId});
        }
    }
}