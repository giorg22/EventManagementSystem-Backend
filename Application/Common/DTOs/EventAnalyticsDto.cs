public record EventAnalyticsDto
{
    public int TotalTicketsSold { get; init; }
    public decimal TotalRevenue { get; init; }
    public double AttendanceRate { get; init; }
    public int ActualAttendance { get; init; }

    public List<TicketTypeStatsDto> StatsByType { get; init; } = new();

    public List<DailySalesDto> DailySales { get; init; } = new();
}

public record TicketTypeStatsDto(string Type, int Count, decimal Revenue);
public record DailySalesDto(DateTime Date, int Count, decimal Revenue);