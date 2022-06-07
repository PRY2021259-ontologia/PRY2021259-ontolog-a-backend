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
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<SuggestionType> SuggestionTypes { get; set; }
        public DbSet<CategoryDisease> CategoryDiseases { get; set; }
        public DbSet<PlantDisease> PlantDiseases { get; set; }
        public DbSet<UserConceptPlantDisease> UserConceptPlantDiseases { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<SuggestionStatus> SuggestionStatuses { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }

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
            builder.Entity<User>().Property(p => p.DateOfBirth).IsRequired();
            builder.Entity<User>().Property(p => p.Occupation).IsRequired();
            builder.Entity<User>().Property(p => p.IsActive).IsRequired();
            builder.Entity<User>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships
            builder.Entity<UserType>()
                .HasMany(ut => ut.Users)
                .WithOne(ut => ut.UserType)
                .HasForeignKey(ut => ut.UserTypeId);
            builder.Entity<User>()
                .HasOne(ut => ut.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(ut => ut.UserTypeId);

            builder.Entity<UserLogin>()
               .HasOne(ul => ul.User)
               .WithOne(ul => ul.UserLogin)
               .HasForeignKey<User>(ul => ul.UserLoginId);
            builder.Entity<User>()
                .HasOne(ul => ul.UserLogin)
                .WithOne(ul => ul.User)
                .HasForeignKey<User>(ul => ul.UserLoginId);

            // UserConcept Entity

            builder.Entity<UserConcept>().ToTable("UserConcepts");

            // Constraints

            builder.Entity<UserConcept>().HasKey(p => p.Id);
            builder.Entity<UserConcept>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<UserConcept>().Property(p => p.UserConceptTitle).IsRequired();
            builder.Entity<UserConcept>().Property(p => p.UserConceptDescription).IsRequired();
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

            builder.Entity<ConceptType>()
                .HasMany(uc => uc.UserConcepts)
                .WithOne(uc => uc.ConceptType)
                .HasForeignKey(uc => uc.ConceptTypeId);
            builder.Entity<UserConcept>()
                .HasOne(uc => uc.ConceptType)
                .WithMany(uc => uc.UserConcepts)
                .HasForeignKey(uc => uc.ConceptTypeId);

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

            builder.Entity<SuggestionType>()
                .HasMany(us => us.UserSuggestions)
                .WithOne(us => us.SuggestionType)
                .HasForeignKey(us => us.SuggestionTypeId);
            builder.Entity<UserSuggestion>()
                .HasOne(us => us.SuggestionType)
                .WithMany(us => us.UserSuggestions)
                .HasForeignKey(us => us.SuggestionTypeId);

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

            // ConceptType Entity

            builder.Entity<ConceptType>().ToTable("ConceptTypes");

            // Constraints

            builder.Entity<ConceptType>().HasKey(p => p.Id);
            builder.Entity<ConceptType>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<ConceptType>().Property(p => p.ConceptTypeName).IsRequired();
            builder.Entity<ConceptType>().Property(p => p.ConceptTypeDescription).IsRequired();
            builder.Entity<ConceptType>().Property(p => p.IsActive).IsRequired();
            builder.Entity<ConceptType>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<ConceptType>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // UserType Entity

            builder.Entity<UserType>().ToTable("UserTypes");

            // Constraints

            builder.Entity<UserType>().HasKey(p => p.Id);
            builder.Entity<UserType>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<UserType>().Property(p => p.UserTypeName).IsRequired();
            builder.Entity<UserType>().Property(p => p.UserTypeDescription).IsRequired();
            builder.Entity<UserType>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserType>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserType>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // SuggestionType Entity

            builder.Entity<SuggestionType>().ToTable("SuggestionTypes");

            // Constraints

            builder.Entity<SuggestionType>().HasKey(p => p.Id);
            builder.Entity<SuggestionType>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<SuggestionType>().Property(p => p.SuggestionTypeName).IsRequired();
            builder.Entity<SuggestionType>().Property(p => p.SuggestionTypeDescription).IsRequired();
            builder.Entity<SuggestionType>().Property(p => p.IsActive).IsRequired();
            builder.Entity<SuggestionType>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SuggestionType>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // CategoryDisease Entity

            builder.Entity<CategoryDisease>().ToTable("CategoryDiseases");

            // Constraints

            builder.Entity<CategoryDisease>().HasKey(p => p.Id);
            builder.Entity<CategoryDisease>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<CategoryDisease>().Property(p => p.CategoryDiseaseName).IsRequired();
            builder.Entity<CategoryDisease>().Property(p => p.CategoryDiseaseDescription).IsRequired();
            builder.Entity<CategoryDisease>().Property(p => p.IsActive).IsRequired();
            builder.Entity<CategoryDisease>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<CategoryDisease>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // PlantDisease Entity

            builder.Entity<PlantDisease>().ToTable("PlantDiseases");

            // Constraints

            builder.Entity<PlantDisease>().HasKey(p => p.Id);
            builder.Entity<PlantDisease>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<PlantDisease>().Property(p => p.PlantDiseaseName).IsRequired();
            builder.Entity<PlantDisease>().Property(p => p.PlantDiseaseDescription).IsRequired();
            builder.Entity<PlantDisease>().Property(p => p.IsActive).IsRequired();
            builder.Entity<PlantDisease>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PlantDisease>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Relationships

            builder.Entity<CategoryDisease>()
                .HasMany(pd => pd.PlantDiseases)
                .WithOne(pd => pd.CategoryDisease)
                .HasForeignKey(pd => pd.CategoryDiseaseId);
            builder.Entity<PlantDisease>()
                .HasOne(pd => pd.CategoryDisease)
                .WithMany(pd => pd.PlantDiseases)
                .HasForeignKey(pd => pd.CategoryDiseaseId);

            // UserConceptPlantDisease Entity

            builder.Entity<UserConceptPlantDisease>().ToTable("UserConceptPlantDiseases");

            // Constraints

            builder.Entity<UserConceptPlantDisease>().HasKey(p => new { p.UserConceptId, p.PlantDiseaseId });

            // RelationShips

            builder.Entity<UserConceptPlantDisease>()
                .HasOne(pt => pt.UserConcept)
                .WithMany(p => p.UserConceptPlantDiseases)
                .HasForeignKey(pt => pt.UserConceptId);

            builder.Entity<UserConceptPlantDisease>()
                .HasOne(pt => pt.PlantDisease)
                .WithMany(t => t.UserConceptPlantDiseases)
                .HasForeignKey(pt => pt.PlantDiseaseId);

            // UserLogin Entity

            builder.Entity<UserLogin>().ToTable("UserLogins");

            // Constraints

            builder.Entity<UserLogin>().HasKey(p => p.Id);
            builder.Entity<UserLogin>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<UserLogin>().Property(p => p.Username).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.Password).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.IsActive).IsRequired();
            builder.Entity<UserLogin>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<UserLogin>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // SuggestionStatus Entity

            builder.Entity<SuggestionStatus>().ToTable("SuggestionStatuses");

            // Constraints

            builder.Entity<SuggestionStatus>().HasKey(p => p.Id);
            builder.Entity<SuggestionStatus>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<SuggestionStatus>().Property(p => p.SuggestionStatusTitle).IsRequired();
            builder.Entity<SuggestionStatus>().Property(p => p.SuggestionStatusDescription).IsRequired();
            builder.Entity<SuggestionStatus>().Property(p => p.Url).IsRequired();
            builder.Entity<SuggestionStatus>().Property(p => p.IsProcessed).IsRequired();
            builder.Entity<SuggestionStatus>().Property(p => p.IsActive).IsRequired();
            builder.Entity<SuggestionStatus>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SuggestionStatus>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // RelationShips

            builder.Entity<StatusType>()
               .HasMany(st => st.SuggestionStatuses)
               .WithOne(st => st.StatusType)
               .HasForeignKey(st => st.StatusTypeId);
            builder.Entity<SuggestionStatus>()
                .HasOne(ss => ss.StatusType)
                .WithMany(ss => ss.SuggestionStatuses)
                .HasForeignKey(ss => ss.StatusTypeId);

            builder.Entity<UserSuggestion>()
               .HasMany(st => st.SuggestionStatuses)
               .WithOne(st => st.UserSuggestion)
               .HasForeignKey(st => st.UserSuggestionId);
            builder.Entity<SuggestionStatus>()
                .HasOne(ss => ss.UserSuggestion)
                .WithMany(ss => ss.SuggestionStatuses)
                .HasForeignKey(ss => ss.UserSuggestionId);

            // StatusType Entity

            builder.Entity<StatusType>().ToTable("StatusTypes");

            // Constraints

            builder.Entity<StatusType>().HasKey(p => p.Id);
            builder.Entity<StatusType>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Entity<StatusType>().Property(p => p.StatusTypeTitle).IsRequired();
            builder.Entity<StatusType>().Property(p => p.StatusTypeDescription).IsRequired();
            builder.Entity<StatusType>().Property(p => p.IsActive).IsRequired();
            builder.Entity<StatusType>().Property(p => p.CreatedOn).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<StatusType>().Property(p => p.ModifiedOn).IsRequired().ValueGeneratedOnAdd();

            // Naming Conventions Policy

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
