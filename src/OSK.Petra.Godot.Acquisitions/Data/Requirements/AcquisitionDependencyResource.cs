using Godot;

namespace OSK.Petra.Godot.Acquisitions.Data.Requirements;

[GlobalClass]
[Tool]
public partial class AcquisitionDependencyResource: AcquisitionRequirementInformationResource
{
    #region Variables

    [Export]
    public AcquisitionDefinitionResource Dependency { get; set; }

    [Export]
    public int RequiredAcquisitionCount { get; set; }

    #endregion
}
