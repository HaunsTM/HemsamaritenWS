using System.ServiceModel;

namespace WCF.ServiceLibrary.Interfaces
{
    [ServiceContract]
    public interface IBaseDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeHemsamaritenDB();
    }
}