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
    }
}