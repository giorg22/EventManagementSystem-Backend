public interface ITicketRepository : IBaseRepository<Ticket, int>
{
    Task<int> SavePurchaseAsync(Purchase purchase); // Purchase ენტობისთვის
}