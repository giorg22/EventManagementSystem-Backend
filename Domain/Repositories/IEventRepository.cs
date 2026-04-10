public interface IEventRepository : IBaseRepository<Event, int>
{
    Task<Event?> GetEventWithTicketsAndArtistsAsync(int id);

    Task<List<Event>> GetEventsWithTicketsAndArtistsAsync();

    Task<List<Event>> GetUpcomingEventsAsync();

    Task<List<Event>> GetFilteredEventsAsync(string? searchTerm);
}