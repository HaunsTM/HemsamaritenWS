using System.ServiceModel.Web;

namespace WCF.ServiceLibrary.Interfaces
{
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(ITellstickDuplexCallback))]
    public interface ITellstickDuplexService
    {

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "DumpCurrentlyExecutingTellstickJobsNamesToLog")]
        void DumpCurrentlyExecutingTellstickJobsNamesToLog();

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "StartTellstickScheduler")]
        void StartTellstickScheduler();

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "StopTellstickScheduler")]
        void StopTellstickScheduler();

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "CreateAndInitializeTellstickDB")]
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
        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "POST", UriTemplate = "RegisterTellstickDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Tellstick.Model.Unit RegisterTellstickDevice(string name, string locationDesciption, Tellstick.Model.Enums.ProtocolOption protocolOption, Tellstick.Model.Enums.ModelTypeOption modelTypeOption, Tellstick.Model.Enums.ModelManufacturerOption modelManufacturerOption, Tellstick.Model.Enums.Parameter_UnitOption unitOption, Tellstick.Model.Enums.Parameter_HouseOption houseOption);
         
        [OperationContract(IsOneWay = true)]
        void RemoveTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void TurnOnTellstickDevice(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void TurnOffTellstickDevice(int nativeDeviceId);
    }
}