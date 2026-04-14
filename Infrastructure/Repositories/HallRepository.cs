using Microsoft.EntityFrameworkCore;

public class HallReposotory : BaseRepository<Hall, int>, IHallRepository
{
    private readonly ApplicationDbContext _context;
    public HallReposotory(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Hall>> GetAllWithResourcesAsync(CancellationToken ct)
    {
        return await _context.Halls
            .AsNoTracking()
            .Include(h => h.Resources)
            .ToListAsync(ct);
    }

    public async Task<Hall?> GetByIdWithResourcesAsync(int id)
    {
        return await _context.Halls
            .Include(h => h.Resources)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Hall hall, List<Resource> newResources)
    {
        if (hall.Resources.Any())
        {
            _context.Resources.RemoveRange(hall.Resources);
        }

        hall.Resources = newResources;

        _context.Halls.Update(hall);

        await _context.SaveChangesAsync();
    }
}