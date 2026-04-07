using MediatR;

public record GetDailySalesQuery(int EventId, int DaysLimit = 30) : IRequest<List<DailySalesDto>>;

public class GetDailySalesHandler : IRequestHandler<GetDailySalesQuery, List<DailySalesDto>>
{
    private readonly IAnalyticsService _analyticsService;

    public GetDailySalesHandler(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public async Task<List<DailySalesDto>> Handle(GetDailySalesQuery request, CancellationToken ct)
    {
        return await _analyticsService.GetDailySalesAsync(request.EventId, request.DaysLimit);
    }
}