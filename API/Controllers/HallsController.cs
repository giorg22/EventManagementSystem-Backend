using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]")]
public class HallsController : ControllerBase
{
    private readonly IMediator _mediator;
    public HallsController(IMediator mediator) => _mediator = mediator;

    [HttpGet] public async Task<List<HallDto>> GetAll(CancellationToken ct) => await _mediator.Send(new GetHallsQuery(), ct);

    [HttpGet("{id}")] public async Task<ActionResult<HallDto>> GetById(int id, CancellationToken ct) => await _mediator.Send(new GetHallByIdQuery(id), ct) is { } h ? h : NotFound();

    [HttpPost] public async Task<int> Create(CreateHallCommand cmd, CancellationToken ct) => await _mediator.Send(cmd, ct);

    [HttpPut("{id}")] public async Task<IActionResult> Update(int id, UpdateHallCommand cmd, CancellationToken ct) => id != cmd.Id ? BadRequest() : Ok(await _mediator.Send(cmd, ct));

    [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) => Ok(await _mediator.Send(new DeleteHallCommand(id), ct));
}