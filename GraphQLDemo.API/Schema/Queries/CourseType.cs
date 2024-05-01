namespace GraphQLDemo.API.Schema;

using GraphQLDemo.API.Models;


public class CourseType {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public InstructorType Instructor { get; set; }
    public IEnumerable<StudentType> Students { get; set; }

}