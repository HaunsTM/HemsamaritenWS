using System.Linq;

namespace Core.BLL.Interfaces
{
    using Core.Model;

    public interface IUnitDealer
    {
        TellstickUnit UnitBy(int id);
        TellstickUnit UnitByNativeDevice(int nativeDeviceId);
    }
}