namespace Tellstick.BLL.Interfaces
{
    public interface ITellStickZNetCommander
    {
        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        void TurnOn(int nativeDeviceId);

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        void TurnOff(int nativeDeviceId);

        /// <summary>
        /// Refresh bearer token for communication with the Tellstick
        /// </summary>
        /// <returns></returns>
        bool RefreshBearerToken();
    }
}