
using Microsoft.EntityFrameworkCore;

public class ParticipantRepository : BaseRepository<Participant, int>, IParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Participant?> GetByQrAsync(string qrCodeData)
    {
        return await _context.Participants
            .FirstOrDefaultAsync(p => p.QrCodeData == qrCodeData);
    }

    public async Task<List<Participant>> GetParticipantsByEventAsync(int eventId)
    {
        return await ListAsync(p => p.EventId == eventId);
    }

    public async Task<List<Participant>> GetByEventIdAsync(int eventId)
    {
        return await _context.Participants
            .Include(x => x.Ticket)
            .Where(p => p.EventId == eventId)
            .ToListAsync();
    }
}