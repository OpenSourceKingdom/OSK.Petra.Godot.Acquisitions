using Godot;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Requirements.Dependency;
using OSK.Petra.Godot.Acquisitions.Data.Costs;
using OSK.Petra.Godot.Acquisitions.Data.Holds;
using OSK.Petra.Godot.Acquisitions.Data.Requirements;
using OSK.Petra.Godot.Primitives.Data.Nullables;

namespace OSK.Petra.Godot.Acquisitions.Data;

[GlobalClass]
[Tool]
public partial class AcquisitionConfigurationResource: Resource
{
    #region Variables

    [Export]
    private NullableInt _maxAcquisitions;

    [Export]
    private NullableInt _maxConcurrentAcquisitions;

    [Export]
    private NullableInt _maxConcurrentGroupAcquisitions;

    [Export]
    private NullableInt _maxQueueSize;

    [Export]
    private NullableInt _maxGroupQueueSize;

    [Export]
    private AcquisitionRequirementInformationResource[] _requirements;

    [Export]
    private AcquisitionCostProviderResource _costProvider;

    [Export]
    private AcquisitionHoldProviderResource _holdProvider;

    private AcquisitionDefinitionResource _parentDefinition;

    #endregion

    #region Api

    public void SetParentDependency(AcquisitionDefinitionResource parentDefinition)
    {
        _parentDefinition = parentDefinition;
    }

    public AcquisitionChoiceConfiguration ToChoiceConfiguration()
        => new()
        {
            MaxAcquisitionCount = _maxAcquisitions,
            MaxGroupConcurrentAcquisitions = _maxConcurrentGroupAcquisitions,
            MaxGroupPendingAcquisitions = _maxGroupQueueSize,
            Requirements = BuildRequirements(),
            AcquisitionCostProvider = _costProvider?.GetCostProvider(),
            AcquisitionHoldProvider = _holdProvider?.GetHoldProvider()
        };

    #endregion

    #region Helpers

    private IEnumerable<IAcquisitionRequirement> BuildRequirements()
    {
        var dependencies = _requirements.OfType<AcquisitionDependencyResource>()
                                        .Select(requirement => new AcquisitionDependency(requirement.Dependency.Acquireable.Id, requirement.RequiredAcquisitionCount));
        
        if (_parentDefinition is not null)
        {
            dependencies = dependencies.Append(new AcquisitionDependency(_parentDefinition.Acquireable.Id, 1));
        }

        dependencies = dependencies.DistinctBy(requirement => requirement.AcquireableId);

        return [new AcquisitionDependencyRequirement(dependencies)];
    }

    #endregion
}
