public interface IEventService
{
    Task<bool> PublishEventAsync(int eventId);
    Task<bool> IsCapacityReachedAsync(int eventId);
    Task<List<ReviewDto>> GetEventReviews(int eventId);
}
