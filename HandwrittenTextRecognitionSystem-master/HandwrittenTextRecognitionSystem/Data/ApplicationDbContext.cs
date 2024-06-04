using HandwrittenTextRecognitionSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandwrittenTextRecognitionSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentCourse>().HasKey(c => new { c.StudentId, c.CourseId });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            // Teacher - GroupForCourse (One to Many)
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Teacher)
                .WithMany(t => t.Groups)
                .HasForeignKey(g => g.TeacherId);

            // TeacherAssistant - GroupForCourse (One to Many)
            modelBuilder.Entity<Group>()
                .HasMany(g => g.TeacherAssistants)
                .WithMany(t => t.Groups)
                .UsingEntity(j => j.ToTable("TeacherAssistantGroups"));

            // Student - GroupForCourse (Many to Many)
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithMany(s => s.Groups)
                .UsingEntity(j => j.ToTable("StudentGroups"));

            // Teacher - Assignment (One to Many)
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.TeacherId);

            // Assignment - AssignmentSolution (One to Many)
            modelBuilder.Entity<Solution>()
                .HasOne(b => b.Assignment)
                .WithMany(a => a.Solutions)
                .HasForeignKey(b => b.AssignmentId);

            // Student - AssignmentSolution (One to Many)
            modelBuilder.Entity<Solution>()
                .HasOne(b => b.Student)
                .WithMany(s => s.Solutions)
                .HasForeignKey(b => b.StudentId);

            // Student - Post (One to Many)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Teacher)
                .WithMany(s => s.Posts)
                .HasForeignKey(p => p.TeacherId);

            //Composite keys
            modelBuilder.Entity<StudentGroup>()
            .HasKey(od => new { od.StudentId, od.GroupId });
            modelBuilder.Entity<TeacherAssistantGroup>()
            .HasKey(od => new { od.TeacherAssistantId, od.GroupId });

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set;}
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAssistant> TeacherAssistants { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<StudentGroup> GroupStudents { get; set; }
        public DbSet<TeacherAssistantGroup> GroupTeacherAssistants { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Request> Requests { get; set; }

    }
}

