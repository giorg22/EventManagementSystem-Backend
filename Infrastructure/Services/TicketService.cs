using MediatR;
using Microsoft.EntityFrameworkCore;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepo;
    private readonly IPaymentService _paymentProvider;
    private readonly ApplicationDbContext _context;

    public TicketService(ITicketRepository ticketRepo, IPaymentService paymentProvider, ApplicationDbContext context)
    {
        _ticketRepo = ticketRepo;
        _paymentProvider = paymentProvider;
        _context = context;
    }


    public async Task<List<UserTicketDto>> GetUserPurchasesAsync(int userId)
    {
        return await _context.Purchases
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Include(p => p.Ticket)
                .ThenInclude(t => t.Event)
            .GroupBy(p => p.Ticket.EventId)
            .Select(group => new UserTicketDto
            {
                EventTitle = group.First().Ticket.Event.Title,
                EventStartDate = group.First().Ticket.Event.StartDate,

                Quantity = group.Sum(p => p.Quantity),

                TotalAmount = group.Sum(p => p.TotalAmount),

                TicketType = string.Join(", ", group.Select(p => p.Ticket.Type).Distinct()),

                Status = group.First().Status.ToString(),

                QrCodes = _context.Participants
                    .Where(part => part.EventId == group.Key && part.UserId == userId)
                    .Select(part => part.QrCodeData)
                    .ToList()
            })
            .OrderByDescending(dto => dto.EventStartDate)
            .ToListAsync();
    }

    public async Task<string> ProcessTicketPurchaseAsync(int ticketId, int userId, decimal amount)
    {
        // 1. გადახდის შესრულება
        var transactionId = await _paymentProvider.ProcessPaymentAsync(amount);

        if (string.IsNullOrEmpty(transactionId)) throw new Exception("Payment Failed");

        // 2. QR კოდის უნიკალური სტრინგის გენერაცია
        // ეს სტრინგი შემდეგ გამოიყენება QR კოდის ვიზუალის შესაქმნელად
        string qrContent = $"EVENT-{ticketId}-{transactionId}-{DateTime.UtcNow.Ticks}";

        // 3. ბილეთის შენახვა (რეპოზიტორიის საშუალებით)
        var purchase = new Purchase
        {
            TicketId = ticketId,
            UserId = userId,
            TotalAmount = amount,
            PurchaseDate = DateTime.UtcNow,
            Status = PurchaseStatus.Completed
        };

        await _ticketRepo.SavePurchaseAsync(purchase);

        return qrContent;
    }
}