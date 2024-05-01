namespace GraphQLDemo.API.Schema;
using GraphQLDemo.API.Models;


public class CourseInputType {
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }

}