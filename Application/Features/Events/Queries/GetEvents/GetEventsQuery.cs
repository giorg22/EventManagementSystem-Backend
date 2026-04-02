using MediatR;

public class GetEventsQuery : IRequest<List<EventDto>> { }

public class GetEventsHandler : IRequestHandler<GetEventsQuery, List<EventDto>>
{
    private readonly IEventRepository _repo;
    public GetEventsHandler(IEventRepository repo) => _repo = repo;

    public async Task<List<EventDto>> Handle(GetEventsQuery req, CancellationToken ct)
    {
        var events = await _repo.GetEventsWithTicketsAndArtistsAsync();
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Capacity = e.Capacity,
            HallId = e.HallId,
            ImageUrl = e.ImageUrl,
            Tickets = e.Tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Type = t.Type,
                Price = t.Price,
                Remaining = t.RemainingQuantity
            }).ToList(),
            Artists = e.Artists.Select(a => new ArtistDto
            {
                FullName = a.FullName,
                Role = a.Role
            }).ToList()
        }).ToList();
    }
}