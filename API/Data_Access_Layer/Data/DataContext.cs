using API.Entities;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Refugee> ?Refugees { get; set; }
        public DbSet<Benefactor>? Benefactors { get; set; }
        public DbSet<Comrades> ?Comrades { get; set; }
        public DbSet<Accommodation> ?Accommodations { get; set; }
        public DbSet<Request> ?Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Person>()
            //    .Property(e => e.Id)
            //    .ValueGeneratedOnAdd();

            builder.Entity<AppUser>()
                .HasOne(p => p.Refugee)
                .WithOne(x => x.AppUser)
                .HasForeignKey<Refugee>(x => x.AppUserId);

            builder.Entity<AppUser>()
                .HasOne(p => p.Benefactor)
                .WithOne(x => x.AppUser)
                .HasForeignKey<Benefactor>(x => x.AppUserId);

            builder.Entity<AppUser>()
                .HasOne(p => p.Accommodation)
                .WithOne(s => s.AppUser)
                .HasForeignKey<Accommodation>(s => s.BenefactorAppUserID);

            builder.Entity<AppUser>()
               .HasMany(ur => ur.AppRoles)
               .WithOne(u => u.User)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.AppRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
