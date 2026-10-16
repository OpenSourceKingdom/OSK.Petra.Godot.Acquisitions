using Godot;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Provisions.Data;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs.Provisions;

[GlobalClass]
public partial class ProvisionScalingStrategyResource : Resource
{
    #region Variables

    [Export]
    public ProvisionResource Provision { get; set; }

    [Export]
    public FormulaResource Formula { get; set; }

    [Export]
    public ScalingMode ScaleMode { get; set; } = ScalingMode.Replace;

    #endregion
}
