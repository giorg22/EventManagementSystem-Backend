public class Ticket : BaseEntity<int>
{
    public int EventId { get; set; }
    public virtual Event Event { get; set; } = null!;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int RemainingQuantity { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Available;
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}