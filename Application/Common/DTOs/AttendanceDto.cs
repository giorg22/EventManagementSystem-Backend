
public class AttendanceDto
{
    public int TotalTickets { get; set; }
    public int ScannedTickets { get; set; }
    public double AttendanceRate { get; set; } // პროცენტული მაჩვენებელი
    public List<AttendeeStatusDto> RecentScans { get; set; } = new();
}