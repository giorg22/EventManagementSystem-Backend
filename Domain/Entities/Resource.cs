public class Resource : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public ResourceType Type { get; set; } // Enum: Staff, Equipment
    public int HallId { get; set; }
    public virtual Hall Hall { get; set; } = null!;
}