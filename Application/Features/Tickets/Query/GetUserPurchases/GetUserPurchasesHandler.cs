// Query
using MediatR;

public record GetUserPurchasesQuery(int UserId) : IRequest<List<UserTicketDto>>;

// Handler
public class GetUserPurchasesHandler : IRequestHandler<GetUserPurchasesQuery, List<UserTicketDto>>
{
    private readonly ITicketService _ticketService;

    public GetUserPurchasesHandler(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    public async Task<List<UserTicketDto>> Handle(GetUserPurchasesQuery request, CancellationToken cancellationToken)
    {
        return await _ticketService.GetUserPurchasesAsync(request.UserId);
    }
}