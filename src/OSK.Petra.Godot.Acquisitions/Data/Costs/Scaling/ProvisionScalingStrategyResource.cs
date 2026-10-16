using Godot;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Provisions.Data;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs.Scaling;

/// <summary>
/// Provides configuration for creating scaling strategies
/// </summary>
[GlobalClass]
public partial class ProvisionScalingStrategyResource : Resource
{
    #region Variables

    /// <summary>
    /// The provision type that the strategy applies to
    /// </summary>
    [Export]
    public ProvisionResource Provision { get; set; }

    /// <summary>
    /// The scaling formula to utilize when scaling provisions
    /// </summary>
    [Export]
    public FormulaResource Formula { get; set; }

    /// <summary>
    /// Describes how to apply the scaling once calculated
    /// </summary>
    [Export]
    public ScalingMode ScaleMode { get; set; } = ScalingMode.Replace;

    #endregion

    #region Api

    /// <summary>
    /// Creates a provision scaling strategy that is equivalent to the configuration
    /// </summary>
    /// <returns>The converted scaling strategy</returns>
    public ProvisionScalingStrategy ToStrategy()
        => new(Provision.Id, Formula.GetFormula(), ScaleMode);

    #endregion
}
