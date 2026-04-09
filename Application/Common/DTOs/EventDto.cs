public class EventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
    public int HallId { get; set; }
    public string Status { get; set; } = "DRAFT";
    public string? ImageUrl { get; set; }
    public Hall Hall { get; set; }
    public List<TicketDto> Tickets { get; set; } = new();
    public List<ArtistDto> Artists { get; set; } = new();
}

public class ArtistDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Role { get; set; }
}