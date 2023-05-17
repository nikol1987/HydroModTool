using Microsoft.Extensions.Options;

namespace HydroToolChain.App.Configuration;

public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
{
    public event Action OnStateUpdated;
    
    Task Update(Action<T> applyChanges);
}  