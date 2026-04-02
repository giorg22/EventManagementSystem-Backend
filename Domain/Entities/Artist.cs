public class Artist : BaseEntity<int>
{
    public string FullName { get; set; } = string.Empty;
    public string? Role { get; set; } // მაგ: "Main Speaker", "DJ", "Lead Singer"
    public string? ImageUrl { get; set; }

    // Many-to-Many კავშირი Event-თან
    public ICollection<Event> Events { get; set; } = new List<Event>();
}