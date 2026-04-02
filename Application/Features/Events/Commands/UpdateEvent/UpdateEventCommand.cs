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
    public string? ImageUrl { get; init; }
    public List<UpdateTicketDto> Tickets { get; init; } = new();
    public List<UpdateArtistDto> Artists { get; init; } = new();
}

public record UpdateTicketDto(string Type, decimal Price, int Quantity);
public record UpdateArtistDto(string FullName, string Role);