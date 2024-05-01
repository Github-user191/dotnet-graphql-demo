using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema;

public class CourseResponse {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
}