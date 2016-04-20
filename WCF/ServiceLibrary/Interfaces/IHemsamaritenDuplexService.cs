namespace WCF.ServiceLibrary.Interfaces
{
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(IHemsamaritenDuplexCallback))]
    public interface IHemsamaritenDuplexService : ISurveillanceCam2DBDuplexService, ITellstickDuplexService
    {
    }
}