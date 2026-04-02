using MediatR;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, bool>
{
    private readonly IEventRepository _repo;

    public UpdateEventCommandHandler(IEventRepository repo) => _repo = repo;

    public async Task<bool> Handle(UpdateEventCommand request, CancellationToken ct)
    {
        // 1. მოვძებნოთ არსებული ივენთი ბილეთებთან ერთად
        var existingEvent = await _repo.GetEventWithTicketsAndArtistsAsync(request.Id);

        if (existingEvent == null) return false;

        // 2. მონაცემების განახლება (Command -> Entity)
        existingEvent.Title = request.Title;
        existingEvent.Description = request.Description;
        existingEvent.StartDate = request.StartDate;
        existingEvent.EndDate = request.EndDate;
        existingEvent.Capacity = request.Capacity;
        existingEvent.HallId = request.HallId;

        // 3. ბილეთების განახლება (წაშლა და ხელახლა დამატება)
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

        // 4. ბაზაში შენახვა
        await _repo.UpdateAsync(existingEvent);

        return true;
    }
}