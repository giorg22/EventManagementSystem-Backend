public interface IParticipantRepository : IBaseRepository<Participant, int>
{
    Task<Participant?> GetByQrAsync(string qrCodeData);
    Task<List<Participant>> GetParticipantsByEventAsync(int eventId);
    Task<List<Participant>> GetByEventIdAsync(int eventId);
}