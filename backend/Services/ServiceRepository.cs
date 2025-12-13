using backend.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetServicesAsync(ServiceQuery query)
        {
            var dbQuery = _context.Services
                .Include(s => s.Product)
                .Include(s => s.Location)
                .AsQueryable();

            if (query.LocationId.HasValue)
                dbQuery = dbQuery.Where(s => s.LocationId == query.LocationId.Value);

            return await dbQuery.ToListAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services
                .Include(s => s.Product)
                .Include(s => s.Location)
                .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            await _context.Entry(service).Reference(s => s.Product).LoadAsync();
            await _context.Entry(service).Reference(s => s.Location).LoadAsync();
            return service;
        }

        public async Task<bool> UpdateServiceAsync(Service service)
        {
            var existing = await _context.Services.FindAsync(service.ServiceId);
            if (existing == null) return false;

            existing.Status = service.Status;
            existing.DurationMinutes = service.DurationMinutes;
            existing.ProductId = service.ProductId;
            existing.LocationId = service.LocationId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
