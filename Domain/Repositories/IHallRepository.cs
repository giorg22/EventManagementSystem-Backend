public interface IHallRepository : IBaseRepository<Hall, int>
{
    Task<Hall?> GetByIdWithResourcesAsync(int id);
    Task<List<Hall>> GetAllWithResourcesAsync(CancellationToken ct);
    Task UpdateAsync(Hall hall, List<Resource> newResources);
}