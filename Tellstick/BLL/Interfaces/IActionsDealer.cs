using System.Collections.Generic;

namespace Tellstick.BLL.Interfaces
{
    public interface IActionsDealer
    {
        string DbConnectionStringName { get; }

        Tellstick.Model.Action ActionExists(
            int nativeDeviceId,
            Tellstick.Model.Enums.ActionTypeOption actionTypeOption,
            Tellstick.Model.Interfaces.IScheduler scheduler);

        Tellstick.Model.Action RegisterNewManualAction(int nativeDeviceId, Tellstick.Model.Enums.ActionTypeOption actionTypeOption);

        List<Tellstick.Model.Action> GetAllActions();

        List<Tellstick.Model.Action> GetActionsBy(int unitId);
    }
}