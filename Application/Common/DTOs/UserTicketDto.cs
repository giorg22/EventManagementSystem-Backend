public record UserTicketDto
{
    public int PurchaseId { get; init; }
    public string EventTitle { get; init; } = string.Empty;
    public string TicketType { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal TotalAmount { get; init; }
    public DateTime EventStartDate { get; init; }
    public string Status { get; init; } = string.Empty;

    public string TicketHash => $"TKT-{PurchaseId}-{EventStartDate:yyyyMMdd}";

    public UserTicketDto(
    int purchaseId,
    string eventTitle,
    string ticketType,
    int quantity,
    decimal totalAmount,
    DateTime eventStartDate,
    string status)
    {
        PurchaseId = purchaseId;
        EventTitle = eventTitle;
        TicketType = ticketType;
        Quantity = quantity;
        TotalAmount = totalAmount;
        EventStartDate = eventStartDate;
        Status = status;
    }

}