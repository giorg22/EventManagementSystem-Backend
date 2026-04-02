using MediatR;

public class CreateHallCommandHandler : IRequestHandler<CreateHallCommand, int>
{
    private readonly IHallRepository _hallRepo;

    public CreateHallCommandHandler(IHallRepository hallRepo) => _hallRepo = hallRepo;

    public async Task<int> Handle(CreateHallCommand request, CancellationToken ct)
    {
        var hall = new Hall
        {
            Name = request.Name,
            Address = request.Address,
            MaxCapacity = request.MaxCapacity,
            Location = request.Location,
            // EF ავტომატურად მიანიჭებს HallId-ს ამ რესურსებს შენახვისას
            Resources = request.Resources.Select(r => new Resource
            {
                Name = r.Name,
                Type = r.Type
            }).ToList()
        };

        await _hallRepo.AddAsync(hall);

        return hall.Id;
    }
}