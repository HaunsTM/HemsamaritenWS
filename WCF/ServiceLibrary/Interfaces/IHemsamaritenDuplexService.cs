namespace WCF.ServiceLibrary.Interfaces
{
    using System.Security.Cryptography.X509Certificates;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IHemsamaritenDuplexCallback))]
    public interface IHemsamaritenDuplexService : ITellstickDuplexService, IMediaDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeHemsamaritenDB();
    }
}