using MediatR;

public record GetEventAttendanceQuery(int EventId) : IRequest<AttendanceDto>;

public class GetEventAttendanceHandler : IRequestHandler<GetEventAttendanceQuery, AttendanceDto>
{
    private readonly IAnalyticsService _analyticsService;

    public GetEventAttendanceHandler(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    public async Task<AttendanceDto> Handle(GetEventAttendanceQuery request, CancellationToken cancellationToken)
    {
        // პირდაპირ სერვისის გამოძახება
        return await _analyticsService.GetAttendanceStatsAsync(request.EventId);
    }
}