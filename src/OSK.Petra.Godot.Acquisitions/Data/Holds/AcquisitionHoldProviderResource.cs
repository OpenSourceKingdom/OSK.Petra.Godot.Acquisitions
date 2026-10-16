using Godot;
using OSK.Petra.Acquisitions.Ports;

namespace OSK.Petra.Godot.Acquisitions.Data.Holds;

[GlobalClass]
[Tool]
public abstract partial class AcquisitionHoldProviderResource: Resource
{
    public abstract IAcquisitionHoldProvider GetHoldProvider();
}
