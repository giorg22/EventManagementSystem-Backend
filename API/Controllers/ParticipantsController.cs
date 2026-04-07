using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ParticipantsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("verify-qr/{QrCodeData}")]
    public async Task<IActionResult> Verify(string QrCodeData)
    {
        var result = await _mediator.Send(new VerifyQrCommand(QrCodeData));

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<ParticipantDto>>> GetByEvent(int eventId) => await _mediator.Send(new GetParticipantsQuery(eventId));
}