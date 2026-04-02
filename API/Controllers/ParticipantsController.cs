using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ParticipantsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("verify-qr")]
    public async Task<ActionResult<bool>> VerifyQr(VerifyQrCommand command) => await _mediator.Send(command);

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<ParticipantDto>>> GetByEvent(int eventId) => await _mediator.Send(new GetParticipantsQuery(eventId));
}