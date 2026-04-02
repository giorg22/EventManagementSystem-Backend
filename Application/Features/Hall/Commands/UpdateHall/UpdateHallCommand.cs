using MediatR;

public record UpdateHallCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public List<CreateResourceDto> Resources { get; set; } = new();
}