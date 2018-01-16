namespace Core.BLL.Interfaces
{
    using System.Collections.Generic;
    using Core.Model;

    public interface IActionSearchParameters
    {
        string unitId { get; set; }
        string actionTypeOption { get; set; }
        string[] cronExpressions { get; set; }
    }

    public interface IActionsDealer
    {
        string DbConnectionStringName { get; }

        Core.Model.TellstickAction ActionExists(
            int nativeDeviceId,
            Core.Model.Enums.ActionTypeOption actionTypeOption,
            Core.Model.Interfaces.IScheduler scheduler);

        Core.Model.TellstickAction RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.ActionTypeOption actionTypeOption);

        List<Core.Model.TellstickAction> ActivateActionsFor(IActionSearchParameters searchParameters);
        
    }
}