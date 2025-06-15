using System.Collections.Generic;
using Db.Generation.ResourcesParameters.Impl;

namespace Db.Generation.ResourcesParameters
{
    public interface IResourcesParameters
    {
        IReadOnlyList<ResourceConfig> Resources { get; }
        int TotalResourcesToSpawn { get; }
        int MaxAttemptsPerResource { get; }
        float MinDistanceBetween { get; }
    }
}