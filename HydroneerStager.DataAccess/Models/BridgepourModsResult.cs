using System.Collections.Generic;

namespace HydroModTools.DataAccess.Models
{
    public sealed class BridgepourModsResult
    {
        public BridgepourModsResult(int count,
                                    IReadOnlyCollection<BridgepourMod> results,
                                    int status,
                                    bool success)
        {
            Count = count;
            Results = results;
            Status = status;
            Success = success;
        }

        public int Count { get; }

        public IReadOnlyCollection<BridgepourMod> Results { get; }

        public int Status { get; }

        public bool Success { get; }
    }
}
