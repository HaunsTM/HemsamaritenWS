namespace Tellstick.BLL.Interfaces
{
    public interface INativeTellstickCommander
    {
        /// <summary>
        /// Register a Tellstick device to native Tellstick system
        /// </summary>
        /// <param name="name">Kitchen lamp switch</param>
        /// <param name="protocol">Example: "arctech"</param>
        /// <param name="modelType">Example: "codeswitch"</param>
        /// <param name="modelManufacturer">Example: "nexa"</param> 
        /// <param name="unit">Example: "1"</param>
        /// <param name="house">Example: "F"</param>
        /// <returns>Registered device id (NativeDeviceId)</returns>
        int AddDevice(string name, string protocol, string modelType, string modelManufacturer, string unit, string house);

        /// <summary>
        /// Removes a device.
        /// </summary>
        /// <param name="deviceId">Id of device to remove</param>
        /// <returns>True on success, false otherwise</returns>
        bool RemoveDevice(int deviceId);

        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        bool TurnOn(int deviceId);

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        bool TurnOff(int deviceId);

        /// <summary>
        /// Dims a device.
        /// Make sure the device supports this by calling tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">The device id to dim</param>
        /// <param name="level">The level the device should dim to. This value should be 0-255</param>
        /// <returns>If dim message were sent</returns>
        bool Dim(int deviceId, char level);
    }
}