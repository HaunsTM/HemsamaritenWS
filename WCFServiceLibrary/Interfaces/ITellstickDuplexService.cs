namespace WCFServiceLibrary.Interfaces
{
    using System.ServiceModel;

    using Tellstick.Model;
    using Tellstick.Model.Enums;

    using Protocol = Tellstick.Model.Enums.Protocol;

    [ServiceContract(CallbackContract = typeof(ITellstickDuplexCallback))]
    public interface ITellstickDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void StartTellstickScheduler();

        [OperationContract(IsOneWay = true)]
        void StopTellstickScheduler();

        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeTellstickDB();
        
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
        [OperationContract(IsOneWay = true)]
        void RegisterTellstickDevice(string name, string locationDesciption, Protocol protocol, ModelType modelType, ModelManufacturer modelManufacturer, Parameter_Unit unit, Parameter_House house);
         
        [OperationContract(IsOneWay = true)]
        void RemoveTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        void TurnOnTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        void TurnOffTellstickDevice(int nativeDeviceId);
    }
}