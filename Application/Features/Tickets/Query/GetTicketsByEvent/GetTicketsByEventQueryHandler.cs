using MediatR;
using Microsoft.EntityFrameworkCore;


public record GetTicketsByEventQuery(int EventId) : IRequest<List<TicketDto>>;

public class GetTicketsByEventQueryHandler : IRequestHandler<GetTicketsByEventQuery, List<TicketDto>>
{
    private readonly ITicketRepository _repo;
    public GetTicketsByEventQueryHandler(ITicketRepository repo) => _repo = repo;

    public async Task<List<TicketDto>> Handle(GetTicketsByEventQuery request, CancellationToken ct)
    {
        return await _repo.GetAll()
            .Where(t => t.EventId == request.EventId)
            .Select(t => new TicketDto
            {
                Id = t.Id,
                Type = t.Type,
                Price = t.Price,
                Remaining = t.RemainingQuantity
            })
            .ToListAsync(ct);
    }
}