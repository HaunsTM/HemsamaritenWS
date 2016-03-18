namespace Tellstick.BLL
{
    using Tellstick.BLL.Interfaces;
    using Tellstick.TelldusNETWrapper;

    using System;

    using log4net;

    public class NativeTellstickCommander : INativeTellstickCommander
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        public int AddDevice(string name, string protocol, string modelType, string modelManufacturer, string unit, string house)
        {
            var nativeDeviceId = -1;
            try
            {
                nativeDeviceId = TelldusNETWrapper.tdAddDevice();
                var nameSet = TelldusNETWrapper.tdSetName(nativeDeviceId, name);
                var protocolSet = TelldusNETWrapper.tdSetProtocol(nativeDeviceId, protocol);
                var combinedModelTypeAndManufacturer = String.Format("{0}:{1}", modelType, modelManufacturer);
                var modelSet = TelldusNETWrapper.tdSetModel(nativeDeviceId, combinedModelTypeAndManufacturer);

                var unitSet = TelldusNETWrapper.tdSetDeviceParameter(nativeDeviceId, "unit", unit);
                var houseSet = TelldusNETWrapper.tdSetDeviceParameter(nativeDeviceId, "house", house);
            }
            catch (Exception ex)
            {
                log.Error("Could not register Tellstick device!", ex);
                throw ex;
            }


            return nativeDeviceId;
        }

        /// <summary>
        /// Removes a device.
        /// </summary>
        /// <param name="deviceId">Id of device to remove</param>
        /// <returns>True on success, false otherwise</returns>
        public bool RemoveDevice(int deviceId)
        {
            var succeededRemovingDevice = false;
            try
            {
                succeededRemovingDevice = TelldusNETWrapper.tdRemoveDevice(deviceId);
            }
            catch (Exception ex)
            {
                log.Error("Could not unregister Tellstick device!", ex);
                throw ex;
            }

            return succeededRemovingDevice;
        }

        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        public bool TurnOn(int deviceId)
        {
            var turnedOnMessageSent = false;

            try
            {
                TelldusNETWrapper.tdTurnOn(deviceId);
                turnedOnMessageSent = true;
            }
            catch (Exception ex)
            {
                log.Error("Could not turn on message Tellstick device!", ex);
                throw ex;
            }


            return turnedOnMessageSent;
        }

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        public bool TurnOff(int deviceId) {

            var turnedOffMessageSent = false;

            try
            {
                TelldusNETWrapper.tdTurnOff(deviceId);
                turnedOffMessageSent = true;
            }
            catch (Exception ex)
            {
                log.Error("Could not send turn off message Tellstick device!", ex);
                throw ex;
            }

            return turnedOffMessageSent;
        }

        /// <summary>
        /// Dims a device.
        /// Make sure the device supports this by calling tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="deviceId">The device id to dim</param>
        /// <param name="level">The level the device should dim to. This value should be 0-255</param>
        /// <returns>If dim message were sent</returns>
        public bool Dim(int deviceId, char level)
        {
            var dimMessageSent = false;

            try
            {
                TelldusNETWrapper.tdDim(deviceId, level);
                dimMessageSent = true;
            }
            catch (Exception ex)
            {
                log.Error("Could not turn on message Tellstick device!", ex);
                throw ex;
            }


            return dimMessageSent;
        }
    }
}
