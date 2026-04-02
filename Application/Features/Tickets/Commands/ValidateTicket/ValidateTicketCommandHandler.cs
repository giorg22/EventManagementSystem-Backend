using MediatR;

public record ValidateTicketCommand(string QrCode) : IRequest<bool>;

public class ValidateTicketCommandHandler : IRequestHandler<ValidateTicketCommand, bool>
{
    private readonly IParticipantRepository _repo;
    public ValidateTicketCommandHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<bool> Handle(ValidateTicketCommand request, CancellationToken ct)
    {
        var participant = await _repo.GetByQrAsync(request.QrCode);

        // თუ ბილეთი არ არსებობს ან უკვე გამოყენებულია (Attendance = true)
        if (participant == null || participant.Attendance) return false;

        participant.Attendance = true;
        await _repo.UpdateAsync(participant);
        return true;
    }
}