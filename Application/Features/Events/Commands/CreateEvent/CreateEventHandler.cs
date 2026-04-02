using MediatR;

public class CreateEventHandler : IRequestHandler<CreateEventCommand, int>
{
    private readonly IEventRepository _repo;
    private readonly IFileService _fileService; // ვიყენებთ ინტერფეისს

    public CreateEventHandler(IEventRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<int> Handle(CreateEventCommand req, CancellationToken ct)
    {
        string? imagePath = null;

        // 1. სურათის შენახვა IFileService-ის მეშვეობით
        if (req.Image != null && req.Image.Length > 0)
        {
            // "events" არის ფოლდერის სახელი, სადაც გინდა რომ ჩაიყაროს პოსტერები
            imagePath = await _fileService.SaveFileAsync(req.Image, "events");
        }

        // 2. Entity-ს შექმნა (Mapping)
        var entity = new Event
        {
            Title = req.Title,
            Description = req.Description,
            ImageUrl = imagePath,
            StartDate = req.StartDate,
            EndDate = req.EndDate,
            Capacity = req.Capacity,
            HallId = req.HallId,
            Status = EventStatus.Draft,

            Tickets = req.Tickets.Select(t => new Ticket
            {
                Type = t.Type,
                Price = t.Price,
                Quantity = t.Quantity,
                RemainingQuantity = t.Quantity
            }).ToList(),

            Artists = req.Artists.Select(a => new Artist
            {
                FullName = a.FullName,
                Role = a.Role
            }).ToList()
        };

        // 3. შენახვა რეპოზიტორიში
        await _repo.AddAsync(entity);

        return entity.Id;
    }
}