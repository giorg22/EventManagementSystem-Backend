using MediatR;

public class UpdateHallCommandHandler : IRequestHandler<UpdateHallCommand, Unit>
{
    private readonly IHallRepository _hallRepository;

    public UpdateHallCommandHandler(IHallRepository hallRepository)
    {
        _hallRepository = hallRepository;
    }

    public async Task<Unit> Handle(UpdateHallCommand request, CancellationToken ct)
    {
        var hall = await _hallRepository.GetByIdWithResourcesAsync(request.Id);

        if (hall == null)
            throw new Exception("Not found");

        hall.Name = request.Name;
        hall.Location = request.Location;
        hall.Address = request.Address;
        hall.MaxCapacity = request.MaxCapacity;

        var newResources = request.Resources.Select(r => new Resource
        {
            Name = r.Name,
            Type = (ResourceType)r.Type,
            HallId = hall.Id
        }).ToList();

        await _hallRepository.UpdateAsync(hall, newResources);

        return Unit.Value;
    }
}