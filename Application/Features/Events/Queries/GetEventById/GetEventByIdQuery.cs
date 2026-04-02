using MediatR;

public record GetEventByIdQuery(int Id) : IRequest<EventDto?>;

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto?>
{
    private readonly IEventRepository _repo;
    public GetEventByIdQueryHandler(IEventRepository repo) => _repo = repo;

    public async Task<EventDto?> Handle(GetEventByIdQuery request, CancellationToken ct)
    {
        var ev = await _repo.GetEventWithTicketsAndArtistsAsync(request.Id);

        if (ev == null) return null;

        return new EventDto
        {
            Id = ev.Id,
            Title = ev.Title,
            Description = ev.Description,
            StartDate = ev.StartDate,
            EndDate = ev.EndDate,
            Capacity = ev.Capacity,
            HallId = ev.HallId,
            ImageUrl = ev.ImageUrl,
            Tickets = ev.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Type = t.Type,
                Price = t.Price,
                Quantity = t.Quantity,
                Remaining = t.RemainingQuantity
            }).ToList(),
            Artists = ev.Artists.Select(a => new ArtistDto
            {
                FullName = a.FullName,
                Role = a.Role
            }).ToList()
        };
    }
}