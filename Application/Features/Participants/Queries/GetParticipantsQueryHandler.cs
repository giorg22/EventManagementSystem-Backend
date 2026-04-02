using MediatR;

public record GetParticipantsQuery(int EventId) : IRequest<List<ParticipantDto>>;

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, List<ParticipantDto>>
{
    private readonly IParticipantRepository _repo;
    public GetParticipantsQueryHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<List<ParticipantDto>> Handle(GetParticipantsQuery request, CancellationToken ct)
    {
        var participants = await _repo.GetByEventIdAsync(request.EventId);
        return participants.Select(p => new ParticipantDto
        {
            Id = p.Id,
            Attendance = p.Attendance,
            RegistrationDate = p.RegistrationDate,
            PaidAmount = p.PaidAmount
        }).ToList();
    }
}

public class ParticipantDto
{
    public int Id { get; set; }
    public bool Attendance { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal PaidAmount { get; set; }
}