using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    public EventsController(IMediator mediator) => _mediator = mediator;

    [HttpGet] // ყველა ღონისძიება
    public async Task<ActionResult<List<EventDto>>> GetAll() => await _mediator.Send(new GetEventsQuery());

    [HttpGet("{id}")] // კონკრეტული ღონისძიება დეტალურად
    public async Task<ActionResult<EventDto>> GetById(int id) => await _mediator.Send(new GetEventByIdQuery(id));

    [HttpPost, Authorize] // ახალი ღონისძიების შექმნა
    public async Task<ActionResult<int>> Create(CreateEventCommand command) => await _mediator.Send(command);

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateEventCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID-ები არ ემთხვევა");
        }

        await _mediator.Send(command);
        return NoContent(); // 204 წარმატებული განახლებისთვის
    }

    [HttpDelete("{id}")] // წაშლა
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteEventCommand(id));
        return NoContent();
    }

    [HttpGet("Reviews/{eventId}")]
    public async Task<ActionResult<List<ReviewDto>>> GetReviews(int eventId)
    {
        var query = new GetEventReviewsQuery(eventId);
        return Ok(await _mediator.Send(query));
    }

    [HttpPost("AddReview")]
    public async Task<ActionResult<ReviewDto>> AddReview([FromBody] CreateReviewCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}