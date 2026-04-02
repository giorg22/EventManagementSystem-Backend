public interface IEventRepository : IBaseRepository<Event, int>
{
    // აბრუნებს ღონისძიებას თავის ბილეთებთან ერთად
    Task<Event?> GetEventWithTicketsAndArtistsAsync(int id);

    Task<List<Event>> GetEventsWithTicketsAndArtistsAsync();

    // აბრუნებს მხოლოდ აქტიურ/მომავალ ღონისძიებებს
    Task<List<Event>> GetUpcomingEventsAsync();

    // აბრუნებს ღონისძიებებს ფილტრაციით (სტატუსის ან სათაურის მიხედვით)
    Task<List<Event>> GetFilteredEventsAsync(string? searchTerm, string? status);
}