using MediatR;

public record PurchaseTicketCommand(int EventId, int TicketId, int UserId) : IRequest<int>;

public class PurchaseTicketCommandHandler : IRequestHandler<PurchaseTicketCommand, int>
{
    private readonly ITicketRepository _ticketRepo;
    private readonly IParticipantRepository _participantRepo;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IPaymentService _paymentService;

    public PurchaseTicketCommandHandler(
        ITicketRepository ticketRepo,
        IParticipantRepository participantRepo,
        IPaymentService paymentService,
        IPurchaseRepository purchaseRepository)
    {
        _ticketRepo = ticketRepo;
        _participantRepo = participantRepo;
        _purchaseRepository = purchaseRepository;
        _paymentService = paymentService;
    }

    public async Task<int> Handle(PurchaseTicketCommand request, CancellationToken ct)
    {
        // 1. ვამოწმებთ ბილეთს და ნაშთს
        var ticket = await _ticketRepo.GetByIdAsync(request.TicketId);
        if (ticket == null || ticket.RemainingQuantity <= 0)
            throw new Exception("ბილეთები ამოიწურა!");

        // 2. გადახდის იმიტაცია
        var transactionId = await _paymentService.ProcessPaymentAsync(ticket.Price);
        if (string.IsNullOrEmpty(transactionId)) throw new Exception("გადახდა ვერ მოხერხდა");

        // 3. ნაშთის შემცირება
        ticket.RemainingQuantity--;
        await _ticketRepo.UpdateAsync(ticket);

        var purchase = new Purchase
        {
            TicketId = request.TicketId,
            UserId = request.UserId,
            Quantity = 1, // რადგან ენდპოინტით 1 ბილეთს ყიდულობ
            TotalAmount = ticket.Price,
            Status = PurchaseStatus.Completed, // გადახდა ხომ წარმატებით დასრულდა
            PurchaseDate = DateTime.UtcNow
        };

        await _purchaseRepository.AddAsync(purchase);

        // 4. მონაწილის შექმნა და QR კოდის გენერაცია (იმიტაცია)
        var participant = new Participant
        {
            EventId = request.EventId,
            UserId = request.UserId,
            TicketId = request.TicketId,
            PaidAmount = ticket.Price,
            QrCodeData = Guid.NewGuid().ToString(), // რეალურში აქ QR ლოგიკა იქნება
            RegistrationDate = DateTime.UtcNow
        };

        var result = await _participantRepo.AddAsync(participant);
        return result.Id;
    }
}