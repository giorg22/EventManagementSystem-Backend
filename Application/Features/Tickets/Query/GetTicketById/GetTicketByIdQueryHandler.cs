using MediatR;

public record GetTicketByIdQuery(int Id) : IRequest<TicketDto?>;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto?>
{
    private readonly ITicketRepository _repo;

    public GetTicketByIdQueryHandler(ITicketRepository repo)
    {
        _repo = repo;
    }

    public async Task<TicketDto?> Handle(GetTicketByIdQuery request, CancellationToken ct)
    {
        var ticket = await _repo.GetByIdAsync(request.Id);

        if (ticket == null) return null;

        return new TicketDto
        {
            Id = ticket.Id,
            Type = ticket.Type,
            Price = ticket.Price,
            Remaining = ticket.RemainingQuantity // დარწმუნდით, რომ DTO-ში ეს ველი გაქვთ
        };
    }
}