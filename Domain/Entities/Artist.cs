public class Artist : BaseEntity<int>
{
    public string FullName { get; set; } = string.Empty;
    public string? Role { get; set; }
    public string? ImageUrl { get; set; }

    public ICollection<Event> Events { get; set; } = new List<Event>();
}