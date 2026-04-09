using Microsoft.EntityFrameworkCore;

public class EventRepository : BaseRepository<Event, int>, IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Event?> GetEventWithTicketsAndArtistsAsync(int id)
    {
        return await _context.Events
            .Include(x => x.Hall)
            .Include(x => x.Artists)
            .Include(e => e.Tickets)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Event>> GetUpcomingEventsAsync()
    {
        return await _context.Events
            .Where(e => e.StartDate >= DateTime.UtcNow)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<List<Event>> GetFilteredEventsAsync(string? searchTerm, string? status)
    {
        var query = _context.Events.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(e => e.Title.Contains(searchTerm) || e.Description.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(e => e.Status.ToString() == status);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Event>> GetEventsWithTicketsAndArtistsAsync()
    {
        return await _context.Events
            .Include(x => x.Artists)
            .Include(e => e.Tickets)
            .Include(x => x.Hall)
            .ToListAsync();
    }
}