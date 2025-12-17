namespace backend.Locations;

public interface ILocationRepository
{
    public Task<Location?> GetLocationByIdAsync(int locationId);
}