using MediatR;

public record GetEventReviewsQuery(int EventId) : IRequest<List<ReviewDto>>;