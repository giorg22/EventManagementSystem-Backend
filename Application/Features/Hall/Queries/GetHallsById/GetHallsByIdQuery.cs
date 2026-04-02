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
        var hall = await _hallRepository.GetByIdWithResourcesAsync(request.Id);

        if (hall == null) return null;

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
                Type = (int)r.Type
            }).ToList()
        };
    }
}