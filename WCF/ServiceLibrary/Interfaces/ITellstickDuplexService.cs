using System.Collections.Generic;
using System.Messaging;
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

        [OperationContract]
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetAllActions")]
        string GetAllActions();

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetActionsBy")]
        string GetActionsBy(string unitId);

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SetActionFor")]
        string SetActionFor(string unitId, string actionType, string[] cronExpressions);

        #endregion

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "CreateAndInitializeTellstickDB")]
        void CreateAndInitializeTellstickDB();
        
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