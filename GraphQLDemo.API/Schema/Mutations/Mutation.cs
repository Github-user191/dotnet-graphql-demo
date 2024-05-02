using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Repository;
using GraphQLDemo.API.Schema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema;

public class Mutation {

    //private readonly List<CourseResponse> _courses;
    private readonly CourseRepository _repository;

    public Mutation(CourseRepository repository) {
        _repository = repository;
    }

    // Creating a Course
    // When a course is created, we want to emit an event using a Subscription
    // The ITopicEventSender Interface is used to publish and trigger subscriptions
    public async Task<CourseResponse> CreateCourse(CourseInputType input, [Service] ITopicEventSender sender) {

        CourseDto courseDto = new CourseDto() {
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        };

        courseDto = await _repository.Create(courseDto);
        
        CourseResponse course = new CourseResponse() {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            InstructorId = courseDto.InstructorId
        };

        // Type Safety for topic names
        // "CourseCreated" is out topic and the CourseResponse is our payload
        await sender.SendAsync(nameof(Subscription.CourseCreated), course);
        return course;
    }
    
    
    // Updating a Course
    public async Task<CourseResponse> UpdateCourse(Guid id, CourseInputType input, [Service] ITopicEventSender sender) {
        
        CourseDto courseDto = new CourseDto() {
            Id = id,
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        };

        courseDto = await _repository.Update(courseDto);

        CourseResponse course = new CourseResponse() {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            InstructorId = courseDto.InstructorId
        };

        // Send an event when a course is updated
        // We use a custom event name that is UNIQUE to publish to the topic.
        string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
        await sender.SendAsync(updateCourseTopic, course);

        return course;
    }
    
    // Delete a Course

    public async Task<bool> DeleteCourse(Guid id) {
        try {
            return await _repository.Delete(id);
        }
        catch (Exception e) {
            return false;
        }
    }
}