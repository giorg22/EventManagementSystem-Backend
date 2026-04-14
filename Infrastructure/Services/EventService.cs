using Microsoft.EntityFrameworkCore;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepo;
    private readonly ApplicationDbContext _context;

    public EventService(IEventRepository eventRepo, ApplicationDbContext context)
    {
        _eventRepo = eventRepo;
        _context = context;
    }
    public async Task<bool> IsCapacityReachedAsync(int eventId)
    {
        var ev = await _eventRepo.GetByIdAsync(eventId);
        if (ev == null) return true;

        return ev.Tickets.Sum(t => t.Quantity) >= ev.Capacity;
    }

    public async Task<List<ReviewDto>> GetEventReviews(int eventId)
    {
        return await _context.Reviews
                    .Where(r => r.EventId == eventId)
                    .OrderByDescending(r => r.CreatedAt)
                    .Select(r => new ReviewDto { Id = r.Id, UserName = r.UserName, Comment = r.Comment, Rating = r.Rating, CreatedAt = r.CreatedAt })
                    .ToListAsync();
    }
}