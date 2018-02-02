namespace Core.BLL.Interfaces
{
    using System.Collections.Generic;
    using Core.Model;

    public interface ITellstickActionSearchParameters
    {
        string unitId { get; set; }
        string actionTypeOption { get; set; }
        string[] cronExpressions { get; set; }
    }

    public interface ITellstickActionsDealer
    {
        string DbConnectionStringName { get; }

        Core.Model.TellstickAction ActionExists(
            int nativeDeviceId,
            Core.Model.Enums.TellstickActionTypeOption actionTypeOption,
            Core.Model.Interfaces.IScheduler scheduler);

        Core.Model.TellstickAction RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption);

        List<Core.Model.TellstickAction> ActivateActionsFor(ITellstickActionSearchParameters searchParameters);
        
    }
}