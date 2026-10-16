using Godot;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Godot.Acquisitions.Data.Costs;
using OSK.Petra.Godot.Acquisitions.Data.Holds;
using OSK.Petra.Godot.Acquisitions.Data.Requirements;
using OSK.Petra.Godot.Acquisitions.Data.Requirements.Dependency;
using OSK.Petra.Godot.Primitives.Data.Nullables;

namespace OSK.Petra.Godot.Acquisitions.Data;

/// <summary>
/// The specific conifugration set for an acquisition choice
/// </summary>
[GlobalClass]
[Tool]
public partial class AcquisitionConfigurationResource: Resource
{
    #region Variables

    /// <summary>
    /// The maximum acquisitions this choice can have
    /// </summary>
    [Export]
    private NullableInt _maxAcquisitions;

    /// <summary>
    /// The maximum amount of concurrent acquisitions that can happen within the hub group
    /// </summary>
    [Export]
    private NullableInt _maxGroupConcurrentAcquisitions;

    /// <summary>
    /// The maximum amount of pending acquisitions that can happen within the hub group
    /// </summary>
    [Export]
    private NullableInt _maxGroupPendingAcquisitions;

    /// <summary>
    /// The collection of requirements for this choice. A dependency requirement for parent choices is implicitly added
    /// </summary>
    [Export]
    private AcquisitionRequirementResource[] _requirements;

    /// <summary>
    /// A provider for the costs of an acquisition
    /// </summary>
    [Export]
    private AcquisitionCostProviderResource _costProvider;

    /// <summary>
    /// A provider for the holds of an acquisition
    /// </summary>
    [Export]
    private AcquisitionHoldProviderResource _holdProvider;

    private AcquisitionDefinitionResource _parentDefinition;

    #endregion

    #region Api

    /// <summary>
    /// Sets the parent dependency for this definition resource
    /// </summary>
    /// <param name="parentDefinition">The definition resource that is the parent</param>
    public void SetParentDependency(AcquisitionDefinitionResource? parentDefinition)
    {
        _parentDefinition = parentDefinition;
    }

    /// <summary>
    /// Converts the configuration resource to its equivalent choice configuration
    /// </summary>
    /// <returns>The choice configuration</returns>
    public AcquisitionChoiceConfiguration ToChoiceConfiguration()
        => new()
        {
            MaxAcquisitionCount = _maxAcquisitions,
            MaxGroupConcurrentAcquisitions = _maxGroupConcurrentAcquisitions,
            MaxGroupPendingAcquisitions = _maxGroupPendingAcquisitions,
            Requirements = BuildRequirements(),
            AcquisitionCostProvider = _costProvider?.GetCostProvider(),
            AcquisitionHoldProvider = _holdProvider?.GetHoldProvider()
        };

    #endregion

    #region Helpers

    private IEnumerable<IAcquisitionRequirement> BuildRequirements()
    {
        var validRequirements = _requirements.Where(requirement => requirement is not null);

        if (_parentDefinition is not null)
        {
            validRequirements = validRequirements.Append(new AcquisitionDependencyRequirementResource() 
            { 
                Dependencies = [
                    new AcquisitionDependencyResource() 
                    { 
                        Dependency = _parentDefinition, 
                        RequiredAcquisitionCount = 1 
                    }
                 ] 
            });
        }

        return validRequirements.GroupBy(requuirement => requuirement.GetType()).Where(group => group.Any()).Select(BuildRequirement);
    }

    private IAcquisitionRequirement BuildRequirement(IEnumerable<AcquisitionRequirementResource> requirementGroup)
        => requirementGroup.Aggregate((AcquisitionRequirementResource)null, (accumulated, next) => accumulated?.CombineResource(next) ?? accumulated).GetRequirement();

    #endregion
}
