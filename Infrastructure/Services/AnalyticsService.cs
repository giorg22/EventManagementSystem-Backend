public class AnalyticsService : IAnalyticsService
{
    private readonly IEventRepository _eventRepo;
    private readonly IParticipantRepository _participantRepo;

    public AnalyticsService(IEventRepository eventRepo, IParticipantRepository participantRepo)
    {
        _eventRepo = eventRepo;
        _participantRepo = participantRepo;
    }

    public async Task<EventAnalyticsDto> GetEventStatsAsync(int eventId)
    {
        // მოგვაქვს მონაწილეების სია ამ ღონისძიებისთვის
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        return new EventAnalyticsDto
        {
            // რამდენი ბილეთი გაიყიდა ჯამში
            TotalTicketsSold = participants.Count,

            // ჯამური შემოსავალი (ახლა უკვე იყენებს PaidAmount ველს)
            TotalRevenue = participants.Sum(p => p.PaidAmount),

            // დასწრების პროცენტული მაჩვენებელი
            AttendanceRate = participants.Count > 0
                ? (double)participants.Count(p => p.Attendance) / participants.Count * 100
                : 0
        };
    }
}