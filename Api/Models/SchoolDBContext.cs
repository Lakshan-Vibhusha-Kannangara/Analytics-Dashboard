using Microsoft.EntityFrameworkCore;
using API.Models; // Make sure to include your entity classes

namespace API.Models
{
    public class SchoolDBContext : DbContext
    {
        public SchoolDBContext() { 
            
        }

        public SchoolDBContext(DbContextOptions<SchoolDBContext> options) : base(options) { }
   public DbSet<User> Users { get; set; }
      public DbSet<School> Schools { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<SubjectClass> SubjectClasses { get; set; }
    public DbSet<AssessmentArea> AssessmentAreas { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; } // Add this
  
        public DbSet<SchoolClass> SchoolClasses { get; set; } // Add this
    public DbSet<Award> Awards { get; set; } // Add this

    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurePrimaryKeys(modelBuilder);
            ConfigureUniqueConstraints(modelBuilder);

            base.OnModelCreating(modelBuilder);
            
        }

        private void ConfigurePrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .Property(s => s.SchoolID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

                    modelBuilder.Entity<SchoolClass>()
                .Property(s => s.ID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

            modelBuilder.Entity<Subject>()
                .Property(s => s.SubjectID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

            modelBuilder.Entity<Class>()
                .Property(c => c.ClassID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

            modelBuilder.Entity<SubjectClass>()
                .Property(sc => sc.SubjectClassID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

            modelBuilder.Entity<AssessmentArea>()
                .Property(aa => aa.AreaID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() 
                .IsRequired();

            modelBuilder.Entity<Student>()
                .Property(st => st.StudentID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

            modelBuilder.Entity<Answer>()
                .Property(an => an.AnswerID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();


            modelBuilder.Entity<Award>()
                .Property(aw => aw.AwardID)
                .ValueGeneratedOnAdd()
                .UseMySqlIdentityColumn() // Use this for MySQL
                .IsRequired();

     

        modelBuilder.Entity<Award>()
            .Property(aw => aw.AwardID)
            .ValueGeneratedOnAdd()
            .UseMySqlIdentityColumn()
            .IsRequired();
    

   
        }

        private void ConfigureUniqueConstraints(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<Subject>()
                .HasIndex(s => s.SubjectName)
                .IsUnique();

        
        }
    }
}
