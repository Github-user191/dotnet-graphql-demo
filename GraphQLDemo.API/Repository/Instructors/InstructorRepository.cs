using GraphQLDemo.API.Data;
using GraphQLDemo.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Repository;

public class InstructorRepository {
    // Because we have a pool of DbContexts, we use the factory to give us free instances that are available from the pool
    private readonly IDbContextFactory<ApplicationDbContext> _context;

    public InstructorRepository(IDbContextFactory<ApplicationDbContext> context) {
        _context = context;
    }
    
    // N+1 Problem, a query is made to first get all the instructors (4)
    // Then, another query is made to get all the courses for the instructors
    public async Task<InstructorDto> GetById(Guid id) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            return await context.Instructors
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
    
    // We want to hit the database once to get the courses for the instructors,
    // Then, another query to get all the instructors
    // Use a Data loader to batch the queries into one query 
    public async Task<IEnumerable<InstructorDto>> GetManyByIds(IReadOnlyList<Guid> ids) {
        using (ApplicationDbContext context = _context.CreateDbContext()) {
            // Should hit this one to get all instructors in a single query
            return await context.Instructors
                .Where(i => ids.Contains(i.Id)).ToListAsync();
        }
    }
}