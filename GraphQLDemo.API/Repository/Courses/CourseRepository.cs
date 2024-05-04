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

            // Includes tells EF core to execute joins on the included tables
            // What if there's a query for only a Course, the database will try to fetch the included fields regardless
            // return await context.Courses
            //     .Include(x => x.Instructor) 
            //     .Include(x => x.Students) 
            //     .ToListAsync();

            return await context.Courses
                .ToListAsync();
        }
    }
    
    public async Task<CourseDto> GetById(Guid id) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            // Includes tells EF core to execute joins on the included tables
            // What if there's a query for only a Course, the database will try to fetch the included fields regardless
            // return await context.Courses
            //     .Include(x => x.Instructor)
            //     .Include(x => x.Students) 
            //     .FirstOrDefaultAsync(x => x.Id == id);
            
            return await context.Courses
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