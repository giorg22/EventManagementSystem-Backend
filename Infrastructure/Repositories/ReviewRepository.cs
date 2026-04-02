using Microsoft.EntityFrameworkCore;

public class ReviewRepository : BaseRepository<Review, int>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}