using GraphQLDemo.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Data; 

public class ApplicationDbContext : DbContext {


    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<InstructorDto> Instructors { get; set; }
    public DbSet<StudentDto> Students { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Student has many courses
        // Course has many students
        // Setting a name for the junction table to be created that has composite primary key
        modelBuilder.Entity<StudentDto>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity(j => j.ToTable("StudentCourses"));
    }
}