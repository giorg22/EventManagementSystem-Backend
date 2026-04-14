using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AnalyticsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{eventId}")]
    public async Task<IActionResult> GetFullStats(int eventId)
        => Ok(await _mediator.Send(new GetEventAnalyticsQuery(eventId)));

    [HttpGet("{eventId}/daily-sales")]
    public async Task<IActionResult> GetDailySales(int eventId, [FromQuery] int days = 7)
        => Ok(await _mediator.Send(new GetDailySalesQuery(eventId, days)));

    [HttpGet("{eventId}/ticket-types")]
    public async Task<ActionResult<List<TicketTypeStatsDto>>> GetTicketTypeStats(int eventId)
        => Ok(await _mediator.Send(new GetTicketTypeStatsQuery(eventId)));

    [HttpGet("{eventId}/attendance")]
    public async Task<ActionResult<AttendanceDto>> GetAttendance(int eventId)
        => Ok(await _mediator.Send(new GetEventAttendanceQuery(eventId)));

}