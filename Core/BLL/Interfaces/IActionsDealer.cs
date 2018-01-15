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

        Core.Model.Action ActionExists(
            int nativeDeviceId,
            Core.Model.Enums.ActionTypeOption actionTypeOption,
            Core.Model.Interfaces.IScheduler scheduler);

        Core.Model.Action RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.ActionTypeOption actionTypeOption);

        List<Core.Model.Action> ActivateActionsFor(IActionSearchParameters searchParameters);
        
    }
}