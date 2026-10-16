using Godot;
using OSK.Extensions.Petra.Math.Provisions;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Provisions.Data;
using OSK.Petra.Provisions.Models;
using System.Collections.Generic;
using System.Linq;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs.Scaling;

/// <summary>
/// Provides configuration for a cost provider that is capable of scaling provision costs
/// </summary>
[GlobalClass]
[Tool]
public partial class ScalingProvisionCostProviderResource : AcquisitionCostProviderResource, IAcquisitionCostProvider
{
    #region Variables

    /// <summary>
    /// The initial set of provisions the cost provider uses
    /// </summary>
    [Export]
    private ProvisionResource[] _baseProvisions;

    /// <summary>
    /// A default scaling formula to apply to all provision types
    /// </summary>
    [Export]
    private FormulaResource _defaultScalingFormula;

    /// <summary>
    /// The default scaling mode that describes how to apply the provision scaling
    /// </summary>
    [Export]
    private ScalingMode _defaultScaleMode;

    /// <summary>
    /// A collection of strategies for scaling specific provision types
    /// </summary>
    [Export]
    private ProvisionScalingStrategyResource[] _scalingStrategyOverrides;

    private IProvisionScaler _scaler;

    #endregion

    #region AcquisitionCostProviderResource Overrides

    /// <inheritdoc/>
    public override IAcquisitionCostProvider GetCostProvider()
        => this;

    #endregion

    #region IAcquisitionCostProvider

    /// <inheritdoc/>
    public IEnumerable<Provision> GetCost(HubAcquisitionContext context)
    {
        if (_scaler is null)
        {
            var scalingStrategies = _scalingStrategyOverrides?.Where(formula => formula is { Formula: not null, Provision: not null })
                .DistinctBy(strategy => strategy.Provision.Id)
                .Select(strategy => strategy.ToStrategy());

            if (_defaultScalingFormula is not null)
            {
                scalingStrategies = scalingStrategies.Append(new (_defaultScalingFormula.GetFormula(), _defaultScaleMode));
            }

            _scaler = new ProvisionScaler(scalingStrategies);
        }

        return _scaler.Scale(_baseProvisions.Select(provision => provision.ToProvision()), context.AcquiredAmount);
    }

    #endregion
}
