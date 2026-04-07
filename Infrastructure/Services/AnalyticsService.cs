using Microsoft.EntityFrameworkCore;

public class AnalyticsService : IAnalyticsService
{
    private readonly IParticipantRepository _participantRepo;

    public AnalyticsService(IParticipantRepository participantRepo)
    {
        _participantRepo = participantRepo;
    }

    public async Task<EventAnalyticsDto> GetEventStatsAsync(int eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        if (participants == null || !participants.Any())
        {
            return new EventAnalyticsDto();
        }

        // ვიყენებთ ქვემოთ დაწერილ მეთოდებს, რომ კოდი არ გაორმაგდეს
        var statsByType = await GetStatsByTicketTypeAsync(eventId);
        var dailySales = await GetDailySalesAsync(eventId);

        return new EventAnalyticsDto
        {
            TotalTicketsSold = participants.Count,
            TotalRevenue = participants.Sum(p => p.PaidAmount),
            ActualAttendance = participants.Count(p => p.Attendance),
            AttendanceRate = (double)participants.Count(p => p.Attendance) / participants.Count * 100,
            StatsByType = statsByType,
            DailySales = dailySales
        };
    }

    // ინტერფეისის იმპლემენტაცია: გაყიდვების დინამიკა
    public async Task<List<DailySalesDto>> GetDailySalesAsync(int eventId, int daysLimit = 30)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        if (participants == null) return new List<DailySalesDto>();

        return participants
            .Where(p => p.RegistrationDate >= DateTime.UtcNow.AddDays(-daysLimit))
            .GroupBy(p => p.RegistrationDate.Date)
            .OrderBy(g => g.Key)
            .Select(g => new DailySalesDto(
                g.Key,
                g.Count(),
                g.Sum(p => p.PaidAmount)))
            .ToList();
    }

    // ინტერფეისის იმპლემენტაცია: სტატისტიკა ტიპების მიხედვით
    public async Task<List<TicketTypeStatsDto>> GetStatsByTicketTypeAsync(int eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        if (participants == null) return new List<TicketTypeStatsDto>();

        return participants
            .GroupBy(p => p.Ticket.Type)
            .Select(g => new TicketTypeStatsDto(
                g.Key,
                g.Count(),
                g.Sum(p => p.PaidAmount)))
            .ToList();
    }
}