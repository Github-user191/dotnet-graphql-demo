namespace GraphQLDemo.API.Dtos; 

public class StudentDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }
    // Many to Many for students and courses
    public IEnumerable<CourseDto> Courses { get; set; }
}