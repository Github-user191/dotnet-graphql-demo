using GraphQLDemo.API.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema;

public class Mutation {

    private readonly List<CourseResponse> _courses;

    public Mutation() {
        _courses = new List<CourseResponse>();
    }

    // Creating a Course
    // When a course is created, we want to emit an event using a Subscription
    // The ITopicEventSender Interface is used to publish and trigger subscriptions
    public async Task<CourseResponse> CreateCourse(CourseInputType input, [Service] ITopicEventSender sender) {
        
        CourseResponse course = new CourseResponse() {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        };
        
        _courses.Add(course);

        // Type Safety for topic names
        // "CourseCreated" is out topic and the CourseResponse is our payload
        await sender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }
    
    
    // Updating a Course
    public async Task<CourseResponse> UpdateCourse(Guid id, CourseInputType input, [Service] ITopicEventSender sender) {
        CourseResponse course = _courses.FirstOrDefault(c => c.Id == id);

        // Throwing a GraphQLException exception with message and a code to handle on the client side
        if (course == null) {
            throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
        }

        course.Name = input.Name;
        course.Subject = input.Subject;
        course.InstructorId = input.InstructorId;

        // Send an event when a course is updated
        // We use a custom event name that is UNIQUE to publish to the topic.
        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await sender.SendAsync(updateCourseTopic, course);

        return course;
    }
    
    // Delete a Course

    public bool DeleteCourse(Guid id) {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
    }
}