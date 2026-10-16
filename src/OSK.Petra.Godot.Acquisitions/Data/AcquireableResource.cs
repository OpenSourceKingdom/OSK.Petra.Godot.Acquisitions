using Godot;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Godot.Primitives.Data;

namespace OSK.Petra.Godot.Acquisitions.Data;

/// <summary>
/// The thing that can be acquired
/// </summary>
[GlobalClass]
public partial class AcquireableResource : Resource, IAcquireable
{
    #region Variables

    /// <summary>
    /// The unique id for the acquireable in the application
    /// </summary>
    [Export]
    private GuidIdentifier _identifier;

    #endregion

    #region Api

    /// <inheritdoc/>
    public Guid Id => _identifier;

    /// <summary>
    /// The definition that this acquireable is associated with
    /// </summary>
    public AcquisitionDefinitionResource Definition { get; internal set; }

    #endregion
}
