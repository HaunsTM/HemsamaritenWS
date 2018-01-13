using System.Linq;

namespace Tellstick.BLL.Interfaces
{
    using Tellstick.Model;

    public interface IUnitDealer
    {
        Unit UnitBy(int id);
        Unit UnitByNativeDevice(int nativeDeviceId);
    }
}