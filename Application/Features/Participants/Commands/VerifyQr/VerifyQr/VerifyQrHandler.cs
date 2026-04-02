using MediatR;

public record VerifyQrCommand(string QrCodeData) : IRequest<bool>;

public class VerifyQrCommandHandler : IRequestHandler<VerifyQrCommand, bool>
{
    private readonly IParticipantRepository _repo;
    public VerifyQrCommandHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<bool> Handle(VerifyQrCommand request, CancellationToken ct)
    {
        var participant = await _repo.GetByQrAsync(request.QrCodeData);

        if (participant == null || participant.Attendance)
            return false; // თუ უკვე მოსულია ან არ არსებობს

        participant.Attendance = true;
        await _repo.UpdateAsync(participant);

        return true;
    }
}