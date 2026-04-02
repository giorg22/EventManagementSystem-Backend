using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnalyticsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<EventAnalyticsDto>> GetStats(int eventId) =>
        await _mediator.Send(new GetEventStatsQuery(eventId));
}