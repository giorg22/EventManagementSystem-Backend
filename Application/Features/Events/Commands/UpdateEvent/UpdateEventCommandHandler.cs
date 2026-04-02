using MediatR;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
{
    private readonly IEventRepository _repo;

    public UpdateEventCommandHandler(IEventRepository repo) => _repo = repo;

    public async Task<bool> Handle(UpdateEventCommand request, CancellationToken ct)
    {
        var existingEvent = await _repo.GetEventWithTicketsAndArtistsAsync(request.Id);

        if (existingEvent == null) return false;

        existingEvent.Title = request.Title;
        existingEvent.Description = request.Description;
        existingEvent.StartDate = request.StartDate;
        existingEvent.EndDate = request.EndDate;
        existingEvent.Capacity = request.Capacity;
        existingEvent.HallId = request.HallId;

        if (!string.IsNullOrEmpty(request.ImageUrl))
        {
            existingEvent.ImageUrl = request.ImageUrl;
        }

        existingEvent.Tickets.Clear();
        foreach (var t in request.Tickets)
        {
            existingEvent.Tickets.Add(new Ticket
            {
                Type = t.Type,
                Price = t.Price,
                Quantity = t.Quantity,
                RemainingQuantity = t.Quantity
            });
        }

        existingEvent.Artists.Clear();
        foreach (var a in request.Artists)
        {
            existingEvent.Artists.Add(new Artist
            {
                FullName = a.FullName,
                Role = a.Role
            });
        }

        await _repo.UpdateAsync(existingEvent);

        return true;
    }
}