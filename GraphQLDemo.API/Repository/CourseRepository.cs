using GraphQLDemo.API.Data;
using GraphQLDemo.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Repository;

public class CourseRepository {
    // Because we have a pool of DbContexts, we use the factory to give us free instances that are available from the pool
    private readonly IDbContextFactory<ApplicationDbContext> _context;

    public CourseRepository(IDbContextFactory<ApplicationDbContext> context) {
        _context = context;
    }
    
    public async Task<IEnumerable<CourseDto>> GetAll() {
        using (ApplicationDbContext context = _context.CreateDbContext()) {

            return await context.Courses
                .Include(x => x.Instructor) // Include a join on the Instructor
                .Include(x => x.Students) // Include a join on the Students
                .ToListAsync();
        }
    }
    
    public async Task<CourseDto> GetById(Guid id) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            return await context.Courses
                .Include(x => x.Instructor) // Include a join on the Instructor
                .Include(x => x.Students) // Include a join on the Students
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async Task<CourseDto> Create(CourseDto course) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            context.Courses.Add(course);
            await context.SaveChangesAsync();

            return course;
        }
    }

    public async Task<CourseDto> Update(CourseDto course) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            context.Courses.Update(course);
            await context.SaveChangesAsync();

            return course;
        }
    }
    
    public async Task<bool> Delete(Guid id) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            CourseDto course = new CourseDto() {
                Id = id
            };
            context.Courses.Remove(course);
            return await context.SaveChangesAsync() > 0;
        }
    }
}