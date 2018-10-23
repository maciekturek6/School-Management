using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Exercise_1.Models;

namespace Exercise_1.Identity
{
    public class EFCContext : IdentityDbContext
    {
        public EFCContext(DbContextOptions<EFCContext> opt) : base(opt)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
        //public DbSet<Exercise_1.Models.CalendarModel> CalendarModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            
            base.OnConfiguring(optionsBuilder);

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new LoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);
        }

       
    }
}
