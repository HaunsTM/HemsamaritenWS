namespace Tellstick.BLL.Interfaces
{
    using Tellstick.Model;
    using Tellstick.Model.Enums;
    using Tellstick.Model.Interfaces;

    using Protocol = Tellstick.Model.Enums.Protocol;

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
        Unit AddDevice(string name, string locationDesciption, Protocol protocol, ModelType modelType, ModelManufacturer modelManufacturer, Parameter_Unit unit, Parameter_House house);

        bool RemoveDevice(int nativeDeviceId);

        bool RemoveDevice(IUnit unit);

        bool TurnOnDevice(IUnit unit);

        bool TurnOnDevice(int nativeDeviceId);

        bool TurnOffDevice(IUnit unit);

        bool TurnOffDevice(int nativeDeviceId);
    }
}