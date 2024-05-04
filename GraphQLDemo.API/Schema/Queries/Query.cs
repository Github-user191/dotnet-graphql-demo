using Bogus;
using GraphQLDemo.API.Data;
using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Repository;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Schema;
using GraphQLDemo.API.Models;


public class Query {

    private readonly Faker<InstructorType> _instructorFaker;
    private readonly Faker<StudentType> _studentFaker;
    private readonly Faker<CourseType> _courseFaker;

    // Generating fake data
    // public Query() {
    //     _instructorFaker = new Faker<InstructorType>()
    //         .RuleFor(x => x.Id, Guid.NewGuid())
    //         .RuleFor(x => x.FirstName, c => c.Name.FirstName())
    //         .RuleFor(x => x.LastName, c => c.Name.LastName())
    //         .RuleFor(x => x.Salary, c => c.Random.Double(0, 100000));
    //     
    //     _studentFaker = new Faker<StudentType>()
    //         .RuleFor(x => x.Id, Guid.NewGuid())
    //         .RuleFor(x => x.FirstName, c => c.Name.FirstName())
    //         .RuleFor(x => x.LastName, c => c.Name.LastName())
    //         .RuleFor(x => x.GPA, c => c.Random.Double(1, 4));
    //
    //     _courseFaker = new Faker<CourseType>()
    //         .RuleFor(x => x.Id, Guid.NewGuid())
    //         .RuleFor(x => x.Name, c => c.Name.JobTitle())
    //         .RuleFor(x => x.Subject, c => c.PickRandom<Subject>())
    //         .RuleFor(x => x.Instructor, c => _instructorFaker.Generate())
    //         .RuleFor(x => x.Students, c => _studentFaker.Generate(3));
    // }

    private readonly CourseRepository _repository;


    public Query(CourseRepository repository) {
        _repository = repository;
    }


    [GraphQLDeprecated("This query is deprecated")]
    public string Instructions => "Smash Like!";

    
    public async Task<IEnumerable<CourseType>> GetCourses() {
        //List<CourseType> courses = _courseFaker.Generate(5);
        IEnumerable<CourseDto> courseDtos =  await _repository.GetAll();

        return courseDtos.Select(x => new CourseType() {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
            // Instructor = new InstructorType() {
            //     Id = x.InstructorId,
            //     FirstName = x.Instructor.FirstName,
            //     LastName = x.Instructor.LastName,
            //     Salary = x.Instructor.Salary
            // }
        });
    }
    
    // PAGINATION
    // *******************
    // OFFSET PAGINATION skips a fixed number of items for each page,
    // which can be inefficient for large datasets due to the need to calculate and skip records. 
    
    // CURSOR PAGINATION, on the other hand, relies on unique identifiers (cursors) for each item,
    // fetching data based on comparisons with these cursors, making it more efficient
    //  Acts as a reference point for fetching the next or previous page of results.
    // The client uses these cursors to navigate through the dataset efficiently.
    
    // We apply the pagination against our DB query by injecting the DBContext into the method
    // Return IQueryable representing a db query
    // This will now apply the pagination limit (3 records) into the query instead of GETTING ALL RECORDS AND THEN FILTERING
    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 3)]
    public async Task<IQueryable<CourseType>> GetPaginatedCourses([ScopedService] ApplicationDbContext context) {
        return context.Courses.Select(x => new CourseType() {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
        });
    }
    

    public async Task<IEnumerable<CourseType>> GetCoursesWithDbParameter(ApplicationDbContext context) {
        //List<CourseType> courses = _courseFaker.Generate(5);
        IEnumerable<CourseDto> courseDtos = await context.Courses.ToListAsync();

        return courseDtos.Select(x => new CourseType() {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            InstructorId = x.InstructorId
            // Instructor = new InstructorType() {
            //     Id = x.InstructorId,
            //     FirstName = x.Instructor.FirstName,
            //     LastName = x.Instructor.LastName,
            //     Salary = x.Instructor.Salary
            // }
        });
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id) {
        CourseDto courseDto = await _repository.GetById(id);
        
        return new CourseType() {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            // Instructor = new InstructorType() {
            //     Id = courseDto.InstructorId,
            //     FirstName = courseDto.Instructor.FirstName,
            //     LastName = courseDto.Instructor.LastName,
            //     Salary = courseDto.Instructor.Salary
            // }
        };
    }
}