using System.Collections.Generic;
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

        #region Scheduler

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

        #endregion

        #region Actions

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "Actions")]
        IEnumerable<Tellstick.Model.Action> Actions();

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ActionsBy/{unitId}")]
        IEnumerable<Tellstick.Model.Action> ActionsBy(string unitId);

        #endregion

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "CreateAndInitializeTellstickDB")]
        void CreateAndInitializeTellstickDB();

        #region Add/remove device
        
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

        #endregion

        #region Turn on/off device

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void TurnOnTellstickDeviceNative(int nativeDeviceId);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        void TurnOffTellstickDeviceNative(int nativeDeviceId);
        
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        string TurnOnTellstickDevice(int unitId);
        
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        string TurnOffTellstickDevice(int unitId);
        
        #endregion
    }
}