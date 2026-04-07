public class Participant : BaseEntity<int>
{
    public int EventId { get; set; }
    public virtual Event Event { get; set; } = null!;
    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public int TicketId { get; set; }
    public virtual Ticket Ticket { get; set; } = null!;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public bool Attendance { get; set; } = false;
    public string QrCodeData { get; set; } = string.Empty;
    public decimal PaidAmount { get; set; }
}