namespace Tellstick.BLL
{
    using Tellstick.Model;
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    public interface ITellstickUnitDealer
    {
        string DbConnectionStringName { get; }

        bool TurnOnDevice(IUnit unit);

        bool TurnOnDevice(int nativeDeviceId);

        bool TurnOffDevice(IUnit unit);

        bool TurnOffDevice(int nativeDeviceId);

        bool RefreshBearerToken();

        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        bool ManualTurnOnAndRegisterPerformedActionNative(int nativeDeviceId);

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        bool ManualTurnOffAndRegisterPerformedActionNative(int nativeDeviceId);
    }
}