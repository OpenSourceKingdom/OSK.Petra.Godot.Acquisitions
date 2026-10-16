using Godot;
using OSK.Petra.Acquisitions.Ports;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds;

/// <summary>
/// Provides configuration for an acquisition hold provider
/// </summary>
[GlobalClass]
[Tool]
public abstract partial class AcquisitionHoldProviderResource: Resource
{
    /// <summary>
    /// Gets the provider that is equivalent to the configuration
    /// </summary>
    /// <returns>The hold provider</returns>
    public abstract IAcquisitionHoldProvider GetHoldProvider();
}
