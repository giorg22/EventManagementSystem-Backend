using Microsoft.EntityFrameworkCore;

public class AnalyticsService : IAnalyticsService
{
    private readonly IParticipantRepository _participantRepo;

    public AnalyticsService(IParticipantRepository participantRepo)
    {
        _participantRepo = participantRepo;
    }

    public async Task<AttendanceDto> GetAttendanceStatsAsync(int eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        if (participants == null || !participants.Any())
        {
            return new AttendanceDto();
        }

        var totalTickets = participants.Count;
        var scannedTickets = participants.Count(p => p.Attendance);

        return new AttendanceDto
        {
            TotalTickets = totalTickets,
            ScannedTickets = scannedTickets,
            AttendanceRate = totalTickets > 0 ? (double)scannedTickets / totalTickets * 100 : 0,

            RecentScans = participants
                .Where(p => p.Attendance)
                .OrderByDescending(p => p.RegistrationDate)
                .Take(5)
                .Select(p => new AttendeeStatusDto
                {
                    TicketCode = p.Id.ToString(),
                    TicketType = p.Ticket?.Type ?? "Standard",
                    ScannedAt = DateTime.UtcNow 
                })
                .ToList()
        };
    }

    public async Task<EventAnalyticsDto> GetEventStatsAsync(int eventId)
    {
        var participants = await _participantRepo.GetByEventIdAsync(eventId);

        if (participants == null || !participants.Any())
        {
            return new EventAnalyticsDto();
        }

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