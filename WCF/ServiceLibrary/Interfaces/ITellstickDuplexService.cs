using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCF.ServiceLibrary.Interfaces
{

    [ServiceContract]
    public interface ITellstickDuplexService
    {
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
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "DumpCurrentlyExecutingTellstickJobsNamesToLog")]
        void DumpCurrentlyExecutingTellstickJobsNamesToLog();

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "GET",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RefreshBearerToken")]
        bool RefreshBearerToken();
        
        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "CreateAndInitializeTellstickDB")]
        void CreateAndInitializeTellstickDB();
        
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
        
    }
}