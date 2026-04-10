public interface IEventService
{
    Task<bool> IsCapacityReachedAsync(int eventId);
    Task<List<ReviewDto>> GetEventReviews(int eventId);
}
