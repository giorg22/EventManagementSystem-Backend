using MediatR;

public class ValidateAttendanceCommand : IRequest<bool>
{
    public string QrCodeData { get; set; } = string.Empty;
}