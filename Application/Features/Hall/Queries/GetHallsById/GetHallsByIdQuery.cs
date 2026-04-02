using MediatR;

public class GetHallByIdQueryHandler : IRequestHandler<GetHallByIdQuery, HallDto?>
{
    private readonly IHallRepository _hallRepository;

    public GetHallByIdQueryHandler(IHallRepository hallRepository)
    {
        _hallRepository = hallRepository;
    }

    public async Task<HallDto?> Handle(GetHallByIdQuery request, CancellationToken ct)
    {
        // 1. მონაცემების წამოღება რეპოზიტორიდან
        var hall = await _hallRepository.GetByIdWithResourcesAsync(request.Id);

        // 2. თუ დარბაზი არ მოიძებნა, ვაბრუნებთ null-ს
        if (hall == null) return null;

        // 3. Mapping: Entity -> DTO
        return new HallDto
        {
            Id = hall.Id,
            Name = hall.Name,
            Address = hall.Address,
            Location = hall.Location,
            MaxCapacity = hall.MaxCapacity,
            Resources = hall.Resources.Select(r => new ResourceDto
            {
                Id = r.Id,
                Name = r.Name,
                Type = (int)r.Type // Enum-ის გადაყვანა int-ში (3, 5, 7, 0)
            }).ToList()
        };
    }
}