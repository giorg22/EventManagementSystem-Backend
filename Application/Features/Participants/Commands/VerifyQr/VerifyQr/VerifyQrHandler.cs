using MediatR;

public record VerifyQrCommand(string QrCodeData) : IRequest<VerifyQrResponse>;

public record VerifyQrResponse(bool Success, string Message, string? ParticipantName = null);
public class VerifyQrCommandHandler : IRequestHandler<VerifyQrCommand, VerifyQrResponse>
{
    private readonly IParticipantRepository _repo;
    public VerifyQrCommandHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<VerifyQrResponse> Handle(VerifyQrCommand request, CancellationToken ct)
    {
        var participant = await _repo.GetByQrAsync(request.QrCodeData);

        if (participant == null)
        {
            return new VerifyQrResponse(false, "ბილეთი არასწორია ან ვერ მოიძებნა.");
        }

        if (participant.Attendance)
        {
            return new VerifyQrResponse(false, "ეს ბილეთი უკვე გამოყენებულია!");
        }

        participant.Attendance = true;
        await _repo.UpdateAsync(participant);

        string fullName = $"{participant.User?.FirstName} {participant.User?.LastName}";

        return new VerifyQrResponse(true, "წარმატებული რეგისტრაცია!", fullName);
    }
}