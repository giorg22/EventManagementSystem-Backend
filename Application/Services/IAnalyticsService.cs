using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAnalyticsService
{
    /// <summary>
    /// აბრუნებს კონკრეტული ღონისძიების სრულ ანალიტიკურ მონაცემებს
    /// </summary>
    /// <param name="eventId">ღონისძიების უნიკალური ID</param>
    /// <returns>EventAnalyticsDto ობიექტი სტატისტიკით</returns>
    Task<EventAnalyticsDto> GetEventStatsAsync(int eventId);

    /// <summary>
    /// აბრუნებს გაყიდვების დინამიკას ბოლო დღეების მიხედვით
    /// </summary>
    Task<List<DailySalesDto>> GetDailySalesAsync(int eventId, int daysLimit = 30);

    /// <summary>
    /// აბრუნებს გაყიდვების განაწილებას ბილეთების ტიპების მიხედვით
    /// </summary>
    Task<List<TicketTypeStatsDto>> GetStatsByTicketTypeAsync(int eventId);
}