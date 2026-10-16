using Godot;
using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Godot.Math.Formulas.Data;
using OSK.Petra.Godot.Primitives.Data;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds.Timer;

[GlobalClass]
[Tool]
public partial class TimerHoldProviderResource : AcquisitionHoldProviderResource
{
    #region Variables

    [Export]
    private TimeSpanResource _timer;

    [Export]
    private FormulaResource _timeScaleFormula;

    [Export]
    private ScalingMode _scaleMode;

    #endregion

    #region AcquisitionHoldProviderResource Overrides

    public override IAcquisitionHoldProvider GetHoldProvider()
        => new TimerHoldProvider(_timer, _timeScaleFormula.GetFormula(), _scaleMode);

    #endregion
}
