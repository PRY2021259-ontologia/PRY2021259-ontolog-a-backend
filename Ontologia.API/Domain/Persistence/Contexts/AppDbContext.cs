using Microsoft.EntityFrameworkCore;
using Ontologia.API.Domain.Models;
using Ontologia.API.Extensions;

namespace Ontologia.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserConcept> UserConcepts { get; set; }
        public DbSet<UserSuggestion> UserSuggestions { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }
        public DbSet<ConceptType> ConceptTypes { get; set; }

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

            builder.Entity<User>().Property(p => p.Name).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();
            builder.Entity<User>().Property(p => p.Email).IsRequired();
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

            // UserSuggestion Entity

            builder.Entity<UserSuggestion>().ToTable("UserSuggestions");

            // Constraints

            builder.Entity<UserSuggestion>().HasKey(p => p.Id);
            builder.Entity<UserSuggestion>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserSuggestion>().Property(p => p.Comment).IsRequired();
            builder.Entity<UserSuggestion>().Property(p => p.OptionalEmail).IsRequired();
            builder.Entity<UserSuggestion>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserSuggestion>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserSuggestion>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<User>()
                .HasMany(us => us.UserSuggestions)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);
            builder.Entity<UserSuggestion>()
                .HasOne(us => us.User)
                .WithMany(us => us.UserSuggestions)
                .HasForeignKey(us => us.UserId);

            // UserHistory Entity

            builder.Entity<UserHistory>().ToTable("UserHistories");

            // Constraints

            builder.Entity<UserHistory>().HasKey(p => p.Id);
            builder.Entity<UserHistory>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserHistory>().Property(p => p.Url).IsRequired();
            builder.Entity<UserHistory>().Property(p => p.TextSearched).IsRequired();
            builder.Entity<UserHistory>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserHistory>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserHistory>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<User>()
                .HasMany(uh => uh.UserHistories)
                .WithOne(uh => uh.User)
                .HasForeignKey(uh => uh.UserId);
            builder.Entity<UserHistory>()
                .HasOne(uh => uh.User)
                .WithMany(uh => uh.UserHistories)
                .HasForeignKey(uh => uh.UserId);

            // Naming Conventions Policy

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
