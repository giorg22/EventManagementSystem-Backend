using MediatR;

public class GetHallsQueryHandler : IRequestHandler<GetHallsQuery, List<HallDto>>
{
    private readonly IHallRepository _hallRepo;
    public GetHallsQueryHandler(IHallRepository hallRepo) => _hallRepo = hallRepo;

    public async Task<List<HallDto>> Handle(GetHallsQuery request, CancellationToken ct)
    {
        var halls = await _hallRepo.GetAllWithResourcesAsync(ct);

        return halls.Select(h => new HallDto
        {
            Id = h.Id,
            Name = h.Name,
            Location = h.Location,
            MaxCapacity = h.MaxCapacity,
            Resources = h.Resources.Select(r => new ResourceDto
            {
                Name = r.Name,
                Type = (int)r.Type
            }).ToList()
        }).ToList();
    }
}