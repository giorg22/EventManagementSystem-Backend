using MediatR;

public class CreateEventHandler : IRequestHandler<CreateEventCommand, int>
{
    private readonly IEventRepository _repo;

    public CreateEventHandler(IEventRepository repo)
    {
        _repo = repo;
    }

    public async Task<int> Handle(CreateEventCommand req, CancellationToken ct)
    {
        var entity = new Event
        {
            Title = req.Title,
            Description = req.Description,
            ImageUrl = req.ImageUrl,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            Capacity = req.Capacity,
            HallId = req.HallId,
            Tickets = req.Tickets.Select(t => new Ticket
            {
                Type = t.Type,
                Price = t.Price,
                Quantity = t.Quantity,
                RemainingQuantity = t.Quantity
            }).ToList(),

            Artists = req.Artists.Select(a => new Artist
            {
                FullName = a.FullName,
                Role = a.Role
            }).ToList()
        };

        await _repo.AddAsync(entity);
        return entity.Id;
    }
}