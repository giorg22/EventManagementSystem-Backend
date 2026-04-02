using System.ComponentModel.DataAnnotations.Schema;

public class Purchase : BaseEntity<int>
{
    [ForeignKey("Ticket")]
    public int TicketId { get; set; }
    public virtual Ticket Ticket { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Pending;

}