public record HallDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }

    public List<ResourceDto> Resources { get; init; } = new();
}