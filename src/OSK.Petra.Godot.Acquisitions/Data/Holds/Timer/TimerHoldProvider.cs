using OSK.Extensions.Petra.Math.Provisions.Models;
using OSK.Petra.Acquisitions.Holds.Timers;
using OSK.Petra.Acquisitions.Models;
using OSK.Petra.Acquisitions.Ports;
using OSK.Petra.Math.Formulas;
using System;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds.Timer;

/// <summary>
/// Creates a hold provider that returns timer holds
/// </summary>
/// <param name="timer">The initial timer to use</param>
/// <param name="timeScaleFormula">An optional time scale formula for the timer</param>
/// <param name="scalingMode">How scaling is applied if a formula is used</param>
public class TimerHoldProvider(TimeSpan timer, IFormula timeScaleFormula = null, ScalingMode scalingMode = ScalingMode.Replace) : IAcquisitionHoldProvider
{
    #region IAcquisitionHoldProvider

    /// <inheritdoc/>
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
