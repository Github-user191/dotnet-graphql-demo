using GraphQLDemo.API.Data;
using GraphQLDemo.API.Schema;
using GraphQLDemo.API.Schema.Subscriptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions()
    .AddQueryType<Query>();


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Ensures we have a "pool" of DbContexts ready.
// Allows HotChocolate to safely execute EF operations in parallel
builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(opts => {
    opts.UseSqlite(connectionString);
});


var app = builder.Build();


app.UseRouting();

// Subscriptions use WebSockets instead of HTTP to constantly listen for new events being emitted
app.UseWebSockets();

app.UseEndpoints(endpoints => {
    // Map the GraphQL Server to a single endpoint for clients to use.
    endpoints.MapGraphQL();
});




app.Run();