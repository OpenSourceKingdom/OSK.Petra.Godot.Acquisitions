using Godot;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Godot.Primitives.Data;
using OSK.Petra.Godot.Acquisitions.Data.Requirements;

namespace OSK.Petra.Godot.Acquisitions.Data;

[GlobalClass]
public partial class AcquireableResource : Resource, IAcquireable
{
    #region Variables

    [Export]
    private GuidIdentifier _identifier;

    [Export]
    private string _name;

    [Export]
    private string _description;

    [Export]
    private AcquisitionRequirementInformationResource[] _requirements;

    [Export]
    private Texture2D _icon;

    private List<AcquisitionRequirementInformationResource> _extraRequirements = [];

    #endregion

    #region Api

    public Guid Id => _identifier;

    public string Name => _name;

    public string Description => _description;

    public Texture2D Icon => _icon;

    public AcquisitionDefinitionResource Definition { get; set; }

    #endregion
}
