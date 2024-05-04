using GraphQLDemo.API.DataLoader;
using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Repository;

namespace GraphQLDemo.API.Schema;

using GraphQLDemo.API.Models;


public class CourseType {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    //[GraphQLIgnore] - Don't allow clients to query the field
    // This makes sure we always attach the InstructorId when querying data from the Type
    // For projection we need the FK id in order to fetch related data
    [IsProjected(true)] 
    public Guid InstructorId { get; set; }
    
    // Creating a Resolver directly on the property
    // We only want to fetch the instructor if the client asks for it
    // Adding the [Service] attribute automatically injects the Data Loader without the need to add it in the constructor
    [GraphQLNonNullType]
    public async Task<InstructorType> Instructor([Service] InstructorDataLoader dataLoader) {
        InstructorDto instructorDto = await dataLoader.LoadAsync(InstructorId);

        return new InstructorType() {
            Id = instructorDto.Id,
            FirstName = instructorDto.FirstName,
            LastName = instructorDto.LastName,
            Salary = instructorDto.Salary
        };

    }
    public IEnumerable<StudentType> Students { get; set; }

}