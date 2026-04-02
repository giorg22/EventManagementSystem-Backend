using MediatR;

public record UpdateEventCommand : IRequest<bool>
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int Capacity { get; init; }
    public int HallId { get; init; }
    public List<UpdateTicketCommand> Tickets { get; init; } = new();
}

public record UpdateTicketCommand(string Type, decimal Price, int Quantity);