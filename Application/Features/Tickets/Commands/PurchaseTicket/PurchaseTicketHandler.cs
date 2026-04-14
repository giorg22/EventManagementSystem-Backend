using MediatR;

public record PurchaseTicketCommand(int EventId, int UserId, List<TicketSelection> Selections) : IRequest<int>;

public record TicketSelection(int TicketId, int Quantity);

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
        decimal totalToPay = 0;
        var participantsToAdd = new List<Participant>();
        var purchasesToCreate = new List<Purchase>();

        foreach (var selection in request.Selections)
        {
            var ticket = await _ticketRepo.GetByIdAsync(selection.TicketId);
            if (ticket == null || ticket.RemainingQuantity < selection.Quantity)
                throw new Exception($"ბილეთი {ticket?.Type} ამოწურულია!");

            decimal subTotal = ticket.Price * selection.Quantity;
            totalToPay += subTotal;

            purchasesToCreate.Add(new Purchase
            {
                TicketId = selection.TicketId, 
                UserId = request.UserId,
                Quantity = selection.Quantity,
                TotalAmount = subTotal,
                PurchaseDate = DateTime.UtcNow,
                Status = PurchaseStatus.Completed
            });

            for (int i = 0; i < selection.Quantity; i++)
            {
                participantsToAdd.Add(new Participant
                {
                    EventId = request.EventId,
                    UserId = request.UserId,
                    TicketId = selection.TicketId,
                    PaidAmount = ticket.Price,
                    QrCodeData = Guid.NewGuid().ToString()
                });
            }

            ticket.RemainingQuantity -= selection.Quantity;
            await _ticketRepo.UpdateAsync(ticket);
        }

        await _paymentService.ProcessPaymentAsync(totalToPay);

        foreach (var purchase in purchasesToCreate)
        {
            await _purchaseRepository.AddAsync(purchase);
        }

        foreach (var p in participantsToAdd)
        {
            await _participantRepo.AddAsync(p);
        }

        return purchasesToCreate.LastOrDefault()?.Id ?? 0;
    }
}