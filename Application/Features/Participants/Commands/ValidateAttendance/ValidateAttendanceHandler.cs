using MediatR;

public class ValidateAttendanceHandler : IRequestHandler<ValidateAttendanceCommand, bool>
{
    private readonly IParticipantRepository _repo;
    public ValidateAttendanceHandler(IParticipantRepository repo) => _repo = repo;

    public async Task<bool> Handle(ValidateAttendanceCommand req, CancellationToken ct)
    {
        var participant = await _repo.GetByQrAsync(req.QrCodeData);
        if (participant == null) return false;

        participant.Attendance = true;
        await _repo.UpdateAsync(participant);
        return true;
    }
}