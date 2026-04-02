using MediatR;

public record CreateReviewCommand : IRequest<ReviewDto>
{
    public int EventId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Comment { get; init; } = string.Empty;
    public int Rating { get; init; }
}

public class ReviewDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

}