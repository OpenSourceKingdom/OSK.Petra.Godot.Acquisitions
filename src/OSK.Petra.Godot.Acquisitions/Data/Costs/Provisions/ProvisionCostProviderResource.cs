using Godot;
using OSK.Extensions.Petra.Math.Provisions;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Provisions.Data;
using OSK.Petra.Provisions.Models;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs.Provisions;

[GlobalClass]
[Tool]
public partial class ProvisionCostProviderResource : AcquisitionCostProviderResource, IAcquisitionCostProvider
{
    #region Variables

    [Export]
    private ProvisionResource[] _baseProvisions;

    [Export]
    private FormulaResource _defaultScalingFormula;

    [Export]
    private ScalingMode _defaultScaleMode;

    [Export]
    private ProvisionScalingStrategyResource[] _scalingStrategyOverrides;

    private IProvisionScaler _scaler;

    #endregion

    #region AcquisitionCostProviderResource Overrides

    public override IAcquisitionCostProvider GetCostProvider()
        => this;

    #endregion

    #region IAcquisitionCostProvider

    public IEnumerable<Provision> GetCost(HubAcquisitionContext context)
    {
        if (_scaler is null)
        {
            var scalingStrategies = _scalingStrategyOverrides?.Where(formula => formula is { Formula: not null, Provision: not null })
                .DistinctBy(strategy => strategy.Provision.Id)
                .Select(strategy => new ProvisionScalingStrategy(strategy.Provision.Id, strategy.Formula.GetFormula(), strategy.ScaleMode));

            if (_defaultScalingFormula is not null)
            {
                scalingStrategies = scalingStrategies.Append(new ProvisionScalingStrategy(_defaultScalingFormula.GetFormula(), _defaultScaleMode));
            }

            _scaler = new ProvisionScaler(scalingStrategies);
        }

        return _scaler.Scale(_baseProvisions.Select(provision => provision.ToProvision()), context.AcquiredAmount);
    }

    #endregion
}
