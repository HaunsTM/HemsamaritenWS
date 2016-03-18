namespace Tellstick.BLL.Interfaces
{
    using Tellstick.Model;
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    public interface ITellstickUnitDealer
    {
        INativeTellstickCommander NativeCommander { get; }

        string DbConnectionStringName { get; }

        /// <summary>
        /// Register a Tellstick device to native Tellstick system AND database
        /// </summary>
        /// <param name="name">Example: Kitchen lamp switch</param>
        /// <param name="locationDesciption">Example: Over the table in the kitchen</param>
        /// <param name="protocol">Example: "arctech"</param>
        /// <param name="modelType">Example: "codeswitch"</param>
        /// <param name="modelManufacturer">Example: "nexa"</param> 
        /// <param name="unit">Example: "1"</param>
        /// <param name="house">Example: "F"</param>
        /// <returns>Registered device id</returns>
        TellstickUnit AddDevice(string name, string locationDesciption, EnumTellstickProtocol protocol, EnumTellstickModelType modelType, EnumTellstickModelManufacturer modelManufacturer, EnumTellstickParameter_Unit unit, EnumTellstickParameter_House house);

        bool RemoveDevice(int nativeDeviceId);

        bool RemoveDevice(ITellstickUnit tellstickUnit);

        bool TurnOnDevice(ITellstickUnit tellstickUnit);

        bool TurnOnDevice(int nativeDeviceId);

        bool TurnOffDevice(ITellstickUnit tellstickUnit);

        bool TurnOffDevice(int nativeDeviceId);
    }
}