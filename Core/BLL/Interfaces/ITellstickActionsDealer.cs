using System.Collections.Generic;
using Core.Model;
using Core.Model.ViewModel;

namespace Core.BLL.Interfaces
{
    public interface ITellstickActionSearchParameters
    {
        string UnitId { get; set; }
        string ActionTypeOption { get; set; }
        string[] CronExpressions { get; set; }
    }

    public interface ITellstickActionsDealer
    {
        string DbConnectionStringName { get; }

        Core.Model.TellstickAction ActionExists(
            int nativeDeviceId,
            Core.Model.Enums.TellstickActionTypeOption actionTypeOption,
            Core.Model.Interfaces.IScheduler scheduler);

        Core.Model.TellstickAction RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Scheduler scheduler = null);

        List<Core.Model.TellstickAction> ActivateActionsFor(ITellstickActionSearchParameters searchParameters);

        List<RegisteredTellstickAction> GetAllActions();
        List<RegisteredTellstickAction> GetActionsBy(int tellstickUnitId);

        RegisteredTellstickAction AddAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Scheduler scheduler);
        RegisteredTellstickAction AddAction(int tellstickUnitId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, string schedulerCronExpression);
        bool RemoveAction(int actionId);
    }
}