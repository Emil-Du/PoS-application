
using backend.Database;

namespace backend.Locations;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Location?> GetLocationByIdAsync(int locationId)
    {
        return await _context.Locations.FindAsync(locationId);
    }
}