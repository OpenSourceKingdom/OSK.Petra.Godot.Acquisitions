using Godot;
using OSK.Petra.Acquisitions.Requirements.Dependency;

namespace OSK.Petra.Godot.Acquisitions.Data.Requirements.Dependency;

/// <summary>
/// The choice dependency to apply to a requirement
/// </summary>
[GlobalClass]
public partial class AcquisitionDependencyResource: Resource
{
    #region Variables

    /// <summary>
    /// The dependency tied to the choice
    /// </summary>
    [Export]
    public AcquisitionDefinitionResource Dependency { get; set; }

    /// <summary>
    /// The required amount of acquisitions that is expected of the dependency
    /// </summary>
    [Export]
    public int RequiredAcquisitionCount { get; set; }

    #endregion

    #region Api

    /// <summary>
    /// Creates a dependency that is equivalent to this resource
    /// </summary>
    /// <returns>The equivalent dependency</returns>
    public AcquisitionDependency ToDependency()
        => new(Dependency.Acquireable.Id, RequiredAcquisitionCount);

    #endregion
}
