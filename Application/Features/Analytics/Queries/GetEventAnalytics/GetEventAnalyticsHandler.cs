using MediatR;

public record GetEventAnalyticsQuery(int EventId) : IRequest<EventAnalyticsDto>;

public class GetEventAnalyticsHandler : IRequestHandler<GetEventAnalyticsQuery, EventAnalyticsDto>
{
    private readonly IAnalyticsService _analyticsService;

    public GetEventAnalyticsHandler(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public async Task<EventAnalyticsDto> Handle(GetEventAnalyticsQuery request, CancellationToken ct)
    {
        return await _analyticsService.GetEventStatsAsync(request.EventId);
    }
}