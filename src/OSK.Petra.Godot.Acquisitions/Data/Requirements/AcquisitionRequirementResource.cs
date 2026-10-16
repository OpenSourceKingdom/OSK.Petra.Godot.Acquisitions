using Godot;
using OSK.Petra.Acquisitions.Models;

namespace OSK.Petra.Godot.Acquisitions.Data.Requirements;

/// <summary>
/// Provides configuration for an acquisition requirement
/// </summary>
[GlobalClass]
[Tool]
public abstract partial class AcquisitionRequirementResource: Resource
{
    #region Api

    /// <summary>
    /// Combines the other resource with the current
    /// </summary>
    /// <param name="other">The other resource</param>
    /// <returns>The combine requirement resource</returns>
    public abstract AcquisitionRequirementResource CombineResource(AcquisitionRequirementResource other);

    /// <summary>
    /// Gets an equivalent requirement that this resource represents
    /// </summary>
    /// <returns>The requirement that this information represents</returns>
    public abstract IAcquisitionRequirement GetRequirement();

    #endregion
}
