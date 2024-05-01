using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Subscriptions; 

public class Subscription {
    
    // Once you subscribe to a Mutation, you will receive an event when the mutation occurs
   
    
    // This will return the Created Course to the caller when the Mutation is triggered
    // The caller MUST be subscribed to the topic to get notified of events
    [Subscribe]
    // [Topic("CourseCreatedTopic")] Creates a custom named topic to use
    public CourseResponse CourseCreated([EventMessage] CourseResponse course) => course;

    // This subscription is used when you want to listen to events for a specific course using its courseId
    [SubscribeAndResolve]
    public ValueTask<ISourceStream<CourseResponse>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver receiver) {
        string updateCourseTopic = $"{courseId}_{nameof(CourseUpdated)}";

        return receiver.SubscribeAsync<CourseResponse>(updateCourseTopic);
    }
}