using Godot;
using OSK.Petra.Acquisitions.Ports;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs;

/// <summary>
/// Provides configuration for an acquisition cost provider
/// </summary>
[GlobalClass]
[Tool]
public abstract partial class AcquisitionCostProviderResource: Resource
{
    #region Helpers

    /// <summary>
    /// Gets an equivalent cost provider to the resource
    /// </summary>
    /// <returns>The cost provider</returns>
    public abstract IAcquisitionCostProvider GetCostProvider();

    #endregion
}
