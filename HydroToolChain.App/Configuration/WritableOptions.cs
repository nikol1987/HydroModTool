
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HydroToolChain.App.Configuration;

internal class WritableOptions<T> : IWritableOptions<T> where T : class, new()  
{
    public event Action OnStateUpdated = () => {};

    private readonly string _fileRoot = Directory.GetCurrentDirectory();
    
    private readonly IOptionsMonitor<T> _options;  
    private readonly IConfigurationRoot _configuration;  
    private readonly string _file;  

    public WritableOptions(  
        IOptionsMonitor<T> options,  
        IConfigurationRoot configuration,  
        string file)  
    {  
        _options = options;  
        _configuration = configuration;  
        _file = file;  
    }  

    public T Value => _options.CurrentValue;
    
    public async Task Update(Action<T> applyChanges)
    {
        var fileInfo = new FileInfo(Path.Combine(_fileRoot, _file));

        var sectionObject = new T();

        try
        {
            var jObject = JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(fileInfo.FullName));


            if (jObject != null)
            {
                sectionObject = jObject;
            }
        }
        catch (FileNotFoundException)
        {
            // Ignored
        }

        applyChanges(sectionObject);

        await File.WriteAllTextAsync(fileInfo.FullName, JsonConvert.SerializeObject(sectionObject, Formatting.Indented));
        
        _configuration.Reload();
        
        OnStateUpdated.Invoke();
    }  
}