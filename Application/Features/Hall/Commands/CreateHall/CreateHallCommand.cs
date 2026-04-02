using MediatR;

public record CreateHallCommand(
    string Name,
    string Address,
    int MaxCapacity,
    string Location,
    List<CreateResourceDto> Resources) : IRequest<int>;

public record CreateResourceDto(string Name, ResourceType Type);