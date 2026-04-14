using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAnalyticsService
{
    Task<EventAnalyticsDto> GetEventStatsAsync(int eventId);

    Task<List<DailySalesDto>> GetDailySalesAsync(int eventId, int daysLimit = 30);

    Task<List<TicketTypeStatsDto>> GetStatsByTicketTypeAsync(int eventId);

    Task<AttendanceDto> GetAttendanceStatsAsync(int eventId);
}