using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("purchase")]
    public async Task<ActionResult<int>> Purchase(PurchaseTicketCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("validate")]
    public async Task<ActionResult<bool>> Validate(ValidateTicketCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest("ბილეთი არავალიდურია ან უკვე გამოყენებულია.");

        return Ok(result);
    }

    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<TicketDto>>> GetByEvent(int eventId)
    {
        return await _mediator.Send(new GetTicketsByEventQuery(eventId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetTicketByIdQuery(id));
        if (result == null) return NotFound();

        return Ok(result);
    }


    [HttpGet("my-tickets/{userId}")]
    public async Task<IActionResult> GetMyTickets(int userId)
    {
        var result = await _mediator.Send(new GetUserPurchasesQuery(userId));
        return Ok(result);
    }

}