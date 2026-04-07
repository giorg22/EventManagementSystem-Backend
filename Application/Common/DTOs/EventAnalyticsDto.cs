public record EventAnalyticsDto
{
    public int TotalTicketsSold { get; init; }
    public decimal TotalRevenue { get; init; }
    public double AttendanceRate { get; init; }
    public int ActualAttendance { get; init; }

    // გაყიდვები ბილეთების ტიპების მიხედვით (მაგ: VIP: 10, Standard: 50)
    public List<TicketTypeStatsDto> StatsByType { get; init; } = new();

    // გაყიდვების დინამიკა დღეების მიხედვით (გრაფიკისთვის)
    public List<DailySalesDto> DailySales { get; init; } = new();
}

public record TicketTypeStatsDto(string Type, int Count, decimal Revenue);
public record DailySalesDto(DateTime Date, int Count, decimal Revenue);