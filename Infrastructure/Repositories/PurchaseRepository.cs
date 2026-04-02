using Microsoft.EntityFrameworkCore;

public class PurchaseRepository : BaseRepository<Purchase, int>, IPurchaseRepository
{
    public PurchaseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}