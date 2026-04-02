public interface IAnalyticsService
{
    Task<EventAnalyticsDto> GetEventStatsAsync(int eventId);
}