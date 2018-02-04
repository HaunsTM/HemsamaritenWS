using System.ServiceModel.Web;

namespace WCF.ServiceLibrary.Interfaces
{
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IHemsamaritenDuplexCallback))]
    public interface IHemsamaritenDuplexService : ITellstickDuplexService, IMediaDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeHemsamaritenDB();

        #region Scheduler

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "StartAllSchedulers")]
        void StartAllSchedulers();

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "StopAllSchedulers")]
        void StopAllSchedulers();

        #endregion
    }
}