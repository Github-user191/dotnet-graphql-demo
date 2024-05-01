using GraphQLDemo.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Data; 

public class ApplicationDbContext : DbContext {


    public DbSet<CourseDto> Courses;
    public DbSet<InstructorDto> Instructors;
    public DbSet<StudentDto> Students;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        
    }
}