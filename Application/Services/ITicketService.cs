using MediatR;
using Microsoft.EntityFrameworkCore;

public interface ITicketService
{
    Task<string> ProcessTicketPurchaseAsync(int ticketId, int userId, decimal amount);

    Task<List<UserTicketDto>> GetUserPurchasesAsync(int userId);

}