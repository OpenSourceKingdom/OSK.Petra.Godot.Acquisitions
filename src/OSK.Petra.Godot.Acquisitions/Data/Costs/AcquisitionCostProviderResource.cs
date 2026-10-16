using Godot;
using OSK.Petra.Acquisitions.Ports;

namespace OSK.Petra.Godot.Acquisitions.Data.Costs;

[GlobalClass]
[Tool]
public abstract partial class AcquisitionCostProviderResource: Resource
{
    #region Helpers

    public abstract IAcquisitionCostProvider GetCostProvider();

    #endregion
}
