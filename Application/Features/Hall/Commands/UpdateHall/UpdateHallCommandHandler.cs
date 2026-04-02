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
        // 1. წამოვიღოთ დარბაზი რესურსებთან ერთად
        var hall = await _hallRepository.GetByIdWithResourcesAsync(request.Id);

        if (hall == null)
            throw new Exception("Not found");

        // 2. ძირითადი ველების განახლება
        hall.Name = request.Name;
        hall.Location = request.Location;
        hall.Address = request.Address;
        hall.MaxCapacity = request.MaxCapacity;

        // 3. ახალი რესურსების სიის მომზადება (DTO-დან Entity-ში)
        var newResources = request.Resources.Select(r => new Resource
        {
            Name = r.Name,
            Type = (ResourceType)r.Type,
            HallId = hall.Id // Foreign Key-ს მითითება
        }).ToList();

        // 4. რეპოზიტორის გამოძახება
        await _hallRepository.UpdateAsync(hall, newResources);

        return Unit.Value;
    }
}