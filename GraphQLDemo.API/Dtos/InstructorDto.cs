namespace GraphQLDemo.API.Dtos; 

public class InstructorDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }
    
    // Many to Many for instructors and courses
    public IEnumerable<CourseDto> Courses { get; set; }
}