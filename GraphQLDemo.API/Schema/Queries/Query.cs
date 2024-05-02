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

    
    public async Task<IEnumerable<CourseType>> GetCoursesWithRepository() {
        //List<CourseType> courses = _courseFaker.Generate(5);
        IEnumerable<CourseDto> courseDtos =  await _repository.GetAll();

        return courseDtos.Select(x => new CourseType() {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            Instructor = new InstructorType() {
                Id = x.InstructorId,
                FirstName = x.Instructor.FirstName,
                LastName = x.Instructor.LastName,
                Salary = x.Instructor.Salary
            }
        });
    }
    

    public async Task<IEnumerable<CourseType>> GetCoursesWithDbParameter(ApplicationDbContext context) {
        //List<CourseType> courses = _courseFaker.Generate(5);
        IEnumerable<CourseDto> courseDtos = await context.Courses.ToListAsync();

        return courseDtos.Select(x => new CourseType() {
            Id = x.Id,
            Name = x.Name,
            Subject = x.Subject,
            Instructor = new InstructorType() {
                Id = x.InstructorId,
                FirstName = x.Instructor.FirstName,
                LastName = x.Instructor.LastName,
                Salary = x.Instructor.Salary
            }
        });
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id) {
        CourseDto courseDto = await _repository.GetById(id);
        
        return new CourseType() {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            Instructor = new InstructorType() {
                Id = courseDto.InstructorId,
                FirstName = courseDto.Instructor.FirstName,
                LastName = courseDto.Instructor.LastName,
                Salary = courseDto.Instructor.Salary
            }
        };
    }
}