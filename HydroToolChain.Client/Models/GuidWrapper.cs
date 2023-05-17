using System.ComponentModel;
using HydroToolChain.Client.Converters;

namespace HydroToolChain.Client.Models;

[TypeConverter(typeof(GuidWrapperConverter))]
public struct GuidWrapper
{
    public GuidWrapper() : this(Guid.NewGuid())
    {}
    
    public GuidWrapper(Guid guid)
    {
        InternalGuid = guid;
    }
    
    public GuidWrapper(string guid)
    {
        try
        {
            InternalGuid = new Guid(guid);
        }
        catch (Exception)
        {
            InternalGuid = Guid.Empty;
        }
    }

    private Guid InternalGuid { get; } = Guid.Empty;

    public override string ToString()
    {
        return InternalGuid.ToString("N").ToUpper();
    }

    public static implicit operator Guid(GuidWrapper wrapper) => wrapper.InternalGuid;
    public static implicit operator GuidWrapper(Guid guid) => new GuidWrapper(guid);
    public static implicit operator GuidWrapper(string guid)
    {
        try
        {
            return new Guid(guid);
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }
}