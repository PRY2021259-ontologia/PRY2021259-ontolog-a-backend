using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Extensions;

namespace Ontologia.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserConcept> UserConcepts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User Entity

            builder.Entity<User>().ToTable("Users");

            // Constraints

            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(p => p.IsActive).IsRequired();
            builder.Entity<User>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // UserConcept Entity

            builder.Entity<UserConcept>().ToTable("UserConcepts");

            // Constraints

            builder.Entity<UserConcept>().HasKey(p => p.Id);
            builder.Entity<UserConcept>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserConcept>().Property(p => p.Title).IsRequired();
            builder.Entity<UserConcept>().Property(p => p.Description).IsRequired();
            builder.Entity<UserConcept>().Property(p => p.Url).IsRequired();
            builder.Entity<UserConcept>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserConcept>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserConcept>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<User>()
                .HasMany(uc => uc.UserConcepts)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);
            builder.Entity<UserConcept>()
                .HasOne(uc => uc.User)
                .WithMany(uc => uc.UserConcepts)
                .HasForeignKey(uc => uc.UserId);


            // Naming Conventions Policy

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
