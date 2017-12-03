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
        
        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "CreateAndInitializeTellstickDB")]
        void CreateAndInitializeTellstickDB();
        
        #region Turn on/off device

        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        string TurnOnTellstickDevice(string Name);
        
        [WebInvoke(Method = "GET",
                    BodyStyle = WebMessageBodyStyle.WrappedRequest,
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json)]
        string TurnOffTellstickDevice(string Name);
        
        #endregion
    }
}