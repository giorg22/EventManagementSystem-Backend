public record ResourceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    public int Type { get; init; }

    public string TypeName => ((ResourceType)Type).ToString();
}