using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ParticipantsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("verify-qr")] // QR კოდის სკანირება (Attendance = true)
    public async Task<ActionResult<bool>> VerifyQr(VerifyQrCommand command) => await _mediator.Send(command);

    [HttpGet("event/{eventId}")] // მონაწილეების სია (ადმინისთვის)
    public async Task<ActionResult<List<ParticipantDto>>> GetByEvent(int eventId) => await _mediator.Send(new GetParticipantsQuery(eventId));
}