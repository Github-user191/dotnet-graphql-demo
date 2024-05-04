using GraphQLDemo.API.Dtos;
using GraphQLDemo.API.Repository;

namespace GraphQLDemo.API.DataLoader;

// Instead of making separate requests for each parent object,
// DataLoader collects all the requested keys (e.g., IDs of parent objects) and loads them in a single batch from the data source.
// It also caches the results, so if the same key is requested again, DataLoader returns the cached result instead of making another request.
public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDto> {

    private readonly InstructorRepository _repository;

    public InstructorDataLoader(IBatchScheduler batchScheduler,  InstructorRepository repository, DataLoaderOptions? options = null) : base(batchScheduler, options) {
        _repository = repository;
    }
    //
    // The keys represent all Instructor Ids we want to query for
    // Here we get all the instructors for the courses in a batch query
    protected override async Task<IReadOnlyDictionary<Guid, InstructorDto>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken) {
        IEnumerable<InstructorDto> instructors =  await _repository.GetManyByIds(keys);

        return instructors.ToDictionary(i => i.Id);
    }
}