using Bogus;
using HotChocolate;

namespace GraphQLDemo.API.Schema;
using GraphQLDemo.API.Models;


public class Query {

    private readonly Faker<InstructorType> _instructorFaker;
    private readonly Faker<StudentType> _studentFaker;
    private readonly Faker<CourseType> _courseFaker;

    public Query() {
        // Generating fake data
        _instructorFaker = new Faker<InstructorType>()
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.FirstName, c => c.Name.FirstName())
            .RuleFor(x => x.LastName, c => c.Name.LastName())
            .RuleFor(x => x.Salary, c => c.Random.Double(0, 100000));
        
        _studentFaker = new Faker<StudentType>()
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.FirstName, c => c.Name.FirstName())
            .RuleFor(x => x.LastName, c => c.Name.LastName())
            .RuleFor(x => x.GPA, c => c.Random.Double(1, 4));

        _courseFaker = new Faker<CourseType>()
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.Name, c => c.Name.JobTitle())
            .RuleFor(x => x.Subject, c => c.PickRandom<Subject>())
            .RuleFor(x => x.Instructor, c => _instructorFaker.Generate())
            .RuleFor(x => x.Students, c => _studentFaker.Generate(3));
    }


    [GraphQLDeprecated("This query is deprecated")]
    public string Instructions => "Smash Like!";

    
    public IEnumerable<CourseType> GetCourses() {
        List<CourseType> courses = _courseFaker.Generate(5);
        return courses;
    }

    public async Task<CourseType> GetCourseByIdAsync(Guid id) {
        await Task.Delay(1000);
        
        CourseType course = _courseFaker.Generate();
        course.Id = id;
        return course;
    }
}