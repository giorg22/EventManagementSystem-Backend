using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("auth")]
    public async Task<ActionResult<AuthResponse>> Auth(AuthCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}