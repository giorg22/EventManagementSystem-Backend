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

    // POST: api/tickets/purchase
    // ბილეთის შეძენა, გადახდა და QR კოდის გენერაცია
    [HttpPost("purchase")]
    public async Task<ActionResult<int>> Purchase(PurchaseTicketCommand command)
    {
        return await _mediator.Send(command);
    }

    // POST: api/tickets/validate
    // QR კოდის სკანირება და დასწრების აღრიცხვა
    [HttpPost("validate")]
    public async Task<ActionResult<bool>> Validate(ValidateTicketCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result) return BadRequest("ბილეთი არავალიდურია ან უკვე გამოყენებულია.");

        return Ok(result);
    }

    // GET: api/tickets/event/{eventId}
    // კონკრეტული ღონისძიების ყველა ტიპის ბილეთის ნახვა (VIP, Regular...)
    [HttpGet("event/{eventId}")]
    public async Task<ActionResult<List<TicketDto>>> GetByEvent(int eventId)
    {
        return await _mediator.Send(new GetTicketsByEventQuery(eventId));
    }

    // GET: api/tickets/{id}
    // კონკრეტული ბილეთის დეტალების ნახვა ID-ით
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
        // კარგი იქნება თუ აქ შეამოწმებ: User.GetId() == userId (უსაფრთხოებისთვის)
        var result = await _mediator.Send(new GetUserPurchasesQuery(userId));
        return Ok(result);
    }

}