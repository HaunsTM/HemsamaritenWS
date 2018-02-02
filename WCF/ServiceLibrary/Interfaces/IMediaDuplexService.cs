using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Core.Model.ViewModel;

namespace WCF.ServiceLibrary.Interfaces
{
    [ServiceContract(CallbackContract = typeof(IMediaDuplexCallback))]
    public interface IMediaDuplexService
    {
        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string SetVolume(int value);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string Play(string url);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string PlayAndSetVolume(string url, int mediaOutputVolume);

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string StopPlay();

        [WebInvoke(Method = "GET",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<RegisteredMediaSource> PresetMediaSources();
    }
}