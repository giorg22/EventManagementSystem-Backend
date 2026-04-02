public record ResourceDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;

    // აქ ვაბრუნებთ Enum-ის ციფრობრივ მნიშვნელობას (3, 5, 7, 0)
    public int Type { get; init; }

    // სურვილისამებრ: ტიპის დასახელება ტექსტურად (Read-only)
    public string TypeName => ((ResourceType)Type).ToString();
}