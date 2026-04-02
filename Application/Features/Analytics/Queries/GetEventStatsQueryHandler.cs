using MediatR;

public record GetEventStatsQuery(int EventId) : IRequest<EventAnalyticsDto>;

public class GetEventStatsQueryHandler : IRequestHandler<GetEventStatsQuery, EventAnalyticsDto>
{
    private readonly IAnalyticsService _analyticsService;
    public GetEventStatsQueryHandler(IAnalyticsService analyticsService) => _analyticsService = analyticsService;

    public async Task<EventAnalyticsDto> Handle(GetEventStatsQuery request, CancellationToken ct)
    {
        return await _analyticsService.GetEventStatsAsync(request.EventId);
    }
}