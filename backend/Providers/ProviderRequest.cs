namespace backend.Providers;

public class ProviderRequest
{
    public List<int> QualifiedServiceIdsToAdd { get; set; } = new();
    public List<int> QualifiedServiceIdsToRemove { get; set; } = new();
}
