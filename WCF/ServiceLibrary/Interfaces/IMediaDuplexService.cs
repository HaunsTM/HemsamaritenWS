using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Core.Model.ViewModel;

namespace WCF.ServiceLibrary.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IMediaDuplexCallback))]
    public interface IMediaDuplexService
    {

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "StartMediaScheduler")]
        void StartMediaScheduler();

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "StopMediaScheduler")]
        void StopMediaScheduler();

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string SetMediaVolume(int value);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string PlayMedia(string url);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string PlayMediaAndSetVolume(string url, int mediaOutputVolume);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string StopMediaPlay();

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<RegisteredMediaSource> MediaSourcesList();
    }
}