using System.Net.Sockets;

public class Event : BaseEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
    public EventStatus Status { get; set; }
    public int HallId { get; set; }
    public Hall Hall { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}