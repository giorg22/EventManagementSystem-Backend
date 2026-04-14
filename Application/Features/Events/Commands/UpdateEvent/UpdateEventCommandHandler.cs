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

        var ticketsToRemove = existingEvent.Tickets
            .Where(et => !request.Tickets.Any(rt => rt.Id == et.Id) && et.Quantity == et.RemainingQuantity)
            .ToList();

        foreach (var ticket in ticketsToRemove)
        {
            existingEvent.Tickets.Remove(ticket);
        }

        foreach (var rt in request.Tickets)
        {
            var existingTicket = existingEvent.Tickets.FirstOrDefault(et => et.Id == rt.Id);

            if (existingTicket != null)
            {
                existingTicket.Type = rt.Type;
                existingTicket.Price = rt.Price;

                int soldCount = existingTicket.Quantity - existingTicket.RemainingQuantity;
                if (rt.Quantity >= soldCount)
                {
                    existingTicket.Quantity = rt.Quantity;
                    existingTicket.RemainingQuantity = rt.Quantity - soldCount;
                }
            }
            else
            {
                existingEvent.Tickets.Add(new Ticket
                {
                    Type = rt.Type,
                    Price = rt.Price,
                    Quantity = rt.Quantity,
                    RemainingQuantity = rt.Quantity
                });
            }
        }

        existingEvent.Artists.Clear();
        foreach (var a in request.Artists)
        {
            existingEvent.Artists.Add(new Artist { FullName = a.FullName, Role = a.Role });
        }

        await _repo.UpdateAsync(existingEvent);
        return true;
    }
}