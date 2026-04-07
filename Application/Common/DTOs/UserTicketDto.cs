public record UserTicketDto
{
    public int PurchaseId { get; init; }
    public string EventTitle { get; init; } = string.Empty;
    public string TicketType { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime EventStartDate { get; init; }
    public string Status { get; init; } = string.Empty;
    public List<string> QrCodes { get; set; } = new List<string>();
}