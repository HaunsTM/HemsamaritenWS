using System.ServiceModel;

namespace WCF.ServiceLibrary.Interfaces
{
    [ServiceContract]
    public interface IHemsamaritenDuplexService: IBaseDuplexService, IMediaDuplexService, ITellstickDuplexService
    {
    }
}