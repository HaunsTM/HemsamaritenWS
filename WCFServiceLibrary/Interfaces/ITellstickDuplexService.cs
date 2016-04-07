namespace WCFServiceLibrary.Interfaces
{
    using System.ServiceModel;

    using Tellstick.Model;
    using Tellstick.Model.Enums;

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
        /// <param name="protocolOption">Example: "arctech"</param>
        /// <param name="modelTypeOption">Example: "codeswitch"</param>
        /// <param name="modelManufacturerOption">Example: "nexa"</param> 
        /// <param name="unitOption">Example: "1"</param>
        /// <param name="houseOption">Example: "F"</param>
        /// <returns>Registered device id</returns>
        [OperationContract(IsOneWay = true)]
        void RegisterTellstickDevice(string name, string locationDesciption, ProtocolOption protocolOption, ModelTypeOption modelTypeOption, ModelManufacturerOption modelManufacturerOption, Parameter_UnitOption unitOption, Parameter_HouseOption houseOption);
         
        [OperationContract(IsOneWay = true)]
        void RemoveTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        void TurnOnTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        void TurnOffTellstickDevice(int nativeDeviceId);
    }
}