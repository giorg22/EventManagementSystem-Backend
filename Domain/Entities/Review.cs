public class Review : BaseEntity<int>
{
    public int EventId { get; set; }
    public Event Event { get; set; }
    public string UserName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
