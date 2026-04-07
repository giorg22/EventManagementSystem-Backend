using MediatR;

public record VerifyQrCommand(string QrCodeData) : IRequest<VerifyQrResponse>;

public record VerifyQrResponse(bool Success, string Message, string? ParticipantName = null);
public class VerifyQrCommandHandler : IRequestHandler<VerifyQrCommand, VerifyQrResponse>
{
    private readonly IParticipantRepository _repo;
    public VerifyQrCommandHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<VerifyQrResponse> Handle(VerifyQrCommand request, CancellationToken ct)
    {
        // 1. მოძებნე მონაწილე
        var participant = await _repo.GetByQrAsync(request.QrCodeData);

        // 2. თუ ბილეთი საერთოდ არ არსებობს
        if (participant == null)
        {
            return new VerifyQrResponse(false, "ბილეთი არასწორია ან ვერ მოიძებნა.");
        }

        // 3. თუ ბილეთი უკვე დასკანერებულია (Attendance == true)
        if (participant.Attendance)
        {
            return new VerifyQrResponse(false, "ეს ბილეთი უკვე გამოყენებულია!");
        }

        // 4. თუ ყველაფერი რიგზეა, მონიშნე დასწრება
        participant.Attendance = true;
        await _repo.UpdateAsync(participant);

        // წამოვიღოთ სახელი პრეზენტაციისთვის (თუ Participant-ს აქვს User-თან კავშირი)
        string fullName = $"{participant.User?.FirstName} {participant.User?.LastName}";

        return new VerifyQrResponse(true, "წარმატებული რეგისტრაცია!", fullName);
    }
}