public class Hall : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public string Location { get; set; } = string.Empty;
    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}