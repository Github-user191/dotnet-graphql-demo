namespace GraphQLDemo.API.Schema;

public class Mutation {

    private readonly List<CourseResponse> _courses;

    public Mutation() {
        _courses = new List<CourseResponse>();
    }

    // Creating a Course
    public CourseResponse CreateCourse(CourseInputType input) {
        
        CourseResponse course = new CourseResponse() {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        };
        
        _courses.Add(course);

        return course;
    }
    
    
    // Updating a Course
    public CourseResponse UpdateCourse(Guid id, CourseInputType input) {
        CourseResponse course = _courses.FirstOrDefault(c => c.Id == id);

        // Throwing a GraphQLException exception with message and a code to handle on the client side
        if (course == null) {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
        }

        course.Name = input.Name;
        course.Subject = input.Subject;
        course.InstructorId = input.InstructorId;

        return course;
    }
    
    // Delete a Course

    public bool DeleteCourse(Guid id) {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
    }
}