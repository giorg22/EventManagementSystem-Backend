using MediatR;

public record GetTicketTypeStatsQuery(int EventId) : IRequest<List<TicketTypeStatsDto>>;

public class GetTicketTypeStatsHandler : IRequestHandler<GetTicketTypeStatsQuery, List<TicketTypeStatsDto>>
{
    private readonly IAnalyticsService _analyticsService;

    public GetTicketTypeStatsHandler(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public async Task<List<TicketTypeStatsDto>> Handle(GetTicketTypeStatsQuery request, CancellationToken ct)
    {
        return await _analyticsService.GetStatsByTicketTypeAsync(request.EventId);
    }
}