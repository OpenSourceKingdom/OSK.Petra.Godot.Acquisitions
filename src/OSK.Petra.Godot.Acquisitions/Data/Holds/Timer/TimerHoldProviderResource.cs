using Godot;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Primitives.Data;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds.Timer;

/// <summary>
/// A hold provider configuration that utilizes timers
/// </summary>
[GlobalClass]
[Tool]
public partial class TimerHoldProviderResource : AcquisitionHoldProviderResource
{
    #region Variables

    /// <summary>
    /// The timespan for the timer hold that is used
    /// </summary>
    [Export]
    private TimeSpanResource _timer;

    /// <summary>
    /// An optional scaling formula for the timer hold
    /// </summary>
    [Export]
    private FormulaResource _timeScaleFormula;

    /// <summary>
    /// Describes how scaling will be applied to the timer, if a formula is utilized
    /// </summary>
    [Export]
    private ScalingMode _scaleMode;

    #endregion

    #region AcquisitionHoldProviderResource Overrides

    /// <inheritdoc/>
    public override IAcquisitionHoldProvider GetHoldProvider()
        => new TimerHoldProvider(_timer, _timeScaleFormula.GetFormula(), _scaleMode);

    #endregion
}
