
using Azure.Core;
using Microsoft.EntityFrameworkCore;

public class TicketRepository : BaseRepository<Ticket, int>, ITicketRepository
{
    private readonly ApplicationDbContext _context;

    public TicketRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Ticket>> GetTicketsByEventIdAsync(int eventId)
    {
        return await ListAsync(t => t.EventId == eventId);
    }

    public async Task<int> SavePurchaseAsync(Purchase purchase)
    {
        // რადგან Purchase ცალკე ენტობაა და მას თავისი რეპოზიტორია შეიძლება არ ჰქონდეს
        await _context.Set<Purchase>().AddAsync(purchase);
        await _context.SaveChangesAsync();
        return purchase.Id;
    }
}