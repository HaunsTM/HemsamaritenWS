using System.Linq;

namespace Core.BLL.Interfaces
{
    using Core.Model;

    public interface IUnitDealer
    {
        Unit UnitBy(int id);
        Unit UnitByNativeDevice(int nativeDeviceId);
    }
}