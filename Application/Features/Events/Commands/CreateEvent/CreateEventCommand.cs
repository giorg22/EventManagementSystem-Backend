using MediatR;
using Microsoft.AspNetCore.Http;

public class CreateEventCommand : IRequest<int>
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Capacity { get; set; }
    public int HallId { get; set; }
    public string? ImageUrl { get; set; }
    public List<CreateTicketDto> Tickets { get; set; } = new();
    public List<CreateArtistDto> Artists { get; set; } = new();
}

public class CreateTicketDto
{
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class CreateArtistDto
{
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}