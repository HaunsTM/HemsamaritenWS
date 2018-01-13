namespace Tellstick.BLL.Interfaces
{
    using System.Collections.Generic;
    using Tellstick.Model;

    public interface IActionSearchParameters
    {
        string unitId { get; set; }
        string actionTypeOption { get; set; }
        string[] cronExpressions { get; set; }
    }

    public interface IActionsDealer
    {
        string DbConnectionStringName { get; }

        Tellstick.Model.Action ActionExists(
            int nativeDeviceId,
            Tellstick.Model.Enums.ActionTypeOption actionTypeOption,
            Tellstick.Model.Interfaces.IScheduler scheduler);

        Tellstick.Model.Action RegisterNewManualAction(int nativeDeviceId, Tellstick.Model.Enums.ActionTypeOption actionTypeOption);

        List<Tellstick.Model.Action> ActivateActionsFor(IActionSearchParameters searchParameters);
        
    }
}