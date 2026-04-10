using MediatR;

public class GetMyEventsQuery : IRequest<List<EventDto>> 
{
    public int UserId { get; init; }
    public GetMyEventsQuery(int userId)
    {
        UserId = userId;

    }
}

public class GetMyEventsHandler : IRequestHandler<GetMyEventsQuery, List<EventDto>>
{
    private readonly IEventRepository _repo;
    public GetMyEventsHandler(IEventRepository repo) => _repo = repo;

    public async Task<List<EventDto>> Handle(GetMyEventsQuery req, CancellationToken ct)
    {
        var events = await _repo.GetEventsWithTicketsAndArtistsAsync();
        return events
            .Where(x => x.UserId == req.UserId)
            .Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Capacity = e.Capacity,
            HallId = e.HallId,
            ImageUrl = e.ImageUrl,
            Hall = e.Hall,
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