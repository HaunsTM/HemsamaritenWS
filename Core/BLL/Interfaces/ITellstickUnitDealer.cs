using Core.Model.Interfaces;

namespace Core.BLL
{
    public interface ITellstickUnitDealer
    {
        string DbConnectionStringName { get; }

        bool TurnOnDevice(ITellstickUnit unit);

        bool TurnOnDevice(int nativeDeviceId);

        bool TurnOffDevice(ITellstickUnit unit);

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