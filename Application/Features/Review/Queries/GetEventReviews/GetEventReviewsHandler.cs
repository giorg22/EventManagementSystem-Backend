using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetEventReviewsHandler : IRequestHandler<GetEventReviewsQuery, List<ReviewDto>>
{
    private readonly IEventService _eventService;

    public GetEventReviewsHandler(IEventService eventService) => _eventService = eventService;

    public async Task<List<ReviewDto>> Handle(GetEventReviewsQuery request, CancellationToken ct)
    {
        return await _eventService.GetEventReviews(request.EventId);
    }
}