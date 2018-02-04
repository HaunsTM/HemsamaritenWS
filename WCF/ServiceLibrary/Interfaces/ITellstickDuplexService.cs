using System.Collections.Generic;
using System.Messaging;
using System.ServiceModel.Web;
using Core.Model;
using Core.Model.ViewModel;

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

        [OperationContract(IsOneWay = false)]
        [WebInvoke(Method = "POST",
                    RequestFormat = WebMessageFormat.Json,
                    ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "RefreshBearerToken")]
        bool RefreshBearerToken();
        
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

        [WebInvoke(Method = "GET",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        LastPerformedTellstickAction LastPerformedAction(string Name);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<LastPerformedTellstickAction> LastPerformedActionsForAllUnits();
    }
}