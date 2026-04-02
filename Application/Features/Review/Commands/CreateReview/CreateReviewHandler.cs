using MediatR;

public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IReviewRepository _reviewRepo;

    public CreateReviewHandler(IReviewRepository reviewRepo) => _reviewRepo = reviewRepo;

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken ct)
    {
        var review = new Review
        {
            EventId = request.EventId,
            UserName = request.UserName,
            Comment = request.Comment,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow
        };

        await _reviewRepo.AddAsync(review);

        //return new ReviewDto(review.Id, review.UserId, review.Comment, review.Rating, review.CreatedAt);
        return new ReviewDto { Id = review.Id, UserName = review.UserName, Rating = review.Rating, CreatedAt = review.CreatedAt, Comment = review.Comment };
    }
}