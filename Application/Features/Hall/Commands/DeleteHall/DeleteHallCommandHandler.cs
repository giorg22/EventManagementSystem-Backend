using MediatR;

public class DeleteHallCommandHandler : IRequestHandler<DeleteHallCommand, Unit>
{
    private readonly IHallRepository _hallRepo;
    public DeleteHallCommandHandler(IHallRepository hallRepo) => _hallRepo = hallRepo;

    public async Task<Unit> Handle(DeleteHallCommand request, CancellationToken ct)
    {
        var hall = await _hallRepo.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (hall != null)
        {
            await _hallRepo.DeleteAsync(hall);
        }
        return Unit.Value;
    }
}