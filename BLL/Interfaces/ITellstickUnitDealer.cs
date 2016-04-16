namespace Tellstick.BLL
{
    using Tellstick.Model;
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    public interface ITellstickUnitDealer
    {
        string DbConnectionStringName { get; }

        /// <summary>
        /// Register a Tellstick device to native Tellstick system AND database
        /// </summary>
        /// <param name="name">Example: Kitchen lamp switch</param>
        /// <param name="locationDesciption">Example: Over the table in the kitchen</param>
        /// <param name="protocolOption">Example: "arctech"</param>
        /// <param name="modelTypeOption">Example: "codeswitch"</param>
        /// <param name="modelManufacturerOption">Example: "nexa"</param> 
        /// <param name="unitOption">Example: "1"</param>
        /// <param name="houseOption">Example: "F"</param>
        /// <returns>Registered device id</returns>
        Unit AddDevice(string name, string locationDesciption, ProtocolOption protocolOption, ModelTypeOption modelTypeOption, ModelManufacturerOption modelManufacturerOption, Parameter_UnitOption unitOption, Parameter_HouseOption houseOption);

        bool RemoveDevice(int nativeDeviceId);

        bool RemoveDevice(IUnit unit);

        bool TurnOnDevice(IUnit unit);

        bool TurnOnDevice(int nativeDeviceId);

        bool TurnOffDevice(IUnit unit);

        bool TurnOffDevice(int nativeDeviceId);

        /// <summary>
        /// Turns a device on.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        bool ManualTurnOnAndRegisterPerformedAction(int nativeDeviceId);

        /// <summary>
        /// Turns a device off.
        /// Make sure the device supports this by calling TelldusNETWrapper.tdMethods() before any calls to this function.
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        bool ManualTurnOffAndRegisterPerformedAction(int nativeDeviceId);
    }
}