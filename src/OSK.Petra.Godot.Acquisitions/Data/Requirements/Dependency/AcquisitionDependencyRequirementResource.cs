using Godot;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Requirements.Dependency;

namespace OSK.Petra.Godot.Acquisitions.Data.Requirements.Dependency;

/// <summary>
/// Creates a requirement that is associated with dependencies on one or more other choices
/// </summary>
[GlobalClass]
[Tool]
public partial class AcquisitionDependencyRequirementResource: AcquisitionRequirementResource
{
    #region Variables

    /// <summary>
    /// The collection of dependencies tied to the choice
    /// </summary>
    [Export]
    public AcquisitionDependencyResource[] Dependencies { get; set; }

    #endregion

    #region AcquisitionRequirementInformationResource Overrides

    /// <inheritdoc/>
    public override IAcquisitionRequirement GetRequirement()
        => new AcquisitionDependencyRequirement(Dependencies?.Select(dependency => dependency.ToDependency()) ?? []);

    /// <inheritdoc/>
    public override AcquisitionRequirementResource CombineResource(AcquisitionRequirementResource other)
    {
        if (other is null)
        {
            throw new ArgumentNullException(nameof(other));
        }
        if (other is not AcquisitionDependencyRequirementResource dependencyRequirementResource)
        {
            throw new InvalidOperationException($"Unable to combine the resource {GetType().FullName} with another that is not of the same type ({other.GetType().FullName})");
        }

        var combinedDependencies = (Dependencies ?? []).Concat(dependencyRequirementResource.Dependencies ?? [])
                                    .GroupBy(dependency => dependency.Dependency.Acquireable.Id)
                                    .Where(dependencyGroup => dependencyGroup.Any())
                                    .Select(dependencyGroup => new AcquisitionDependencyResource()
                                    {
                                        Dependency = dependencyGroup.First().Dependency,
                                        RequiredAcquisitionCount = dependencyGroup.Max(dependency => dependency.RequiredAcquisitionCount)
                                    });

        return new AcquisitionDependencyRequirementResource()
        {
            Dependencies = [.. combinedDependencies]
        };
    }

    #endregion
}
