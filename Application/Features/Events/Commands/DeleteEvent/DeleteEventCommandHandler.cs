using MediatR;

public record DeleteEventCommand(int Id) : IRequest<Unit>;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IEventRepository _repo;
    public DeleteEventCommandHandler(IEventRepository repo) => _repo = repo;

    public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity != null) await _repo.DeleteAsync(entity);
        return Unit.Value;
    }
}