using GraphQLDemo.API.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();



var app = builder.Build();


app.UseRouting();
app.UseEndpoints(endpoints => {
    // Map the GraphQL Server to a single endpoint for clients to use.
    endpoints.MapGraphQL();
});



app.Run();