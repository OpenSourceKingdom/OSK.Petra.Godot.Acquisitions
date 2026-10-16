using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Holds.Timers;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Math.Formulas;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds.Timer;

public class TimerHoldProvider(TimeSpan timer, IFormula timeScaleFormula, ScalingMode scalingMode) : IAcquisitionHoldProvider
{
    #region IAcquisitionHoldProvider

    public IAcquisitionHold GetHold(HubAcquisitionContext context)
    {
        var timerSeconds = timer.TotalSeconds;
        if (timeScaleFormula is not null)
        {
            var timeScaler = timeScaleFormula.Calculate(context.AcquiredAmount);
            timerSeconds = scalingMode switch
            {
                ScalingMode.Add => timerSeconds + timeScaler,
                ScalingMode.Subtract => timerSeconds - timeScaler,
                ScalingMode.Multiply => timerSeconds * timeScaler,
                ScalingMode.Divide => timeScaler != 0 ? timerSeconds / timeScaler : timerSeconds,
                _ => timerSeconds
            };
        }

        return new TimerHold()
        {
            Timer = TimeSpan.FromSeconds(timerSeconds)
        };
    }

    #endregion
}
