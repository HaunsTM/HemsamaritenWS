namespace WCFServiceLibrary.Interfaces
{
    using System;
    using System.ServiceModel;

    [ServiceContract(CallbackContract = typeof(ISurveillanceCam2DBDuplexCallback))]
    public interface ISurveillanceCam2DBDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void StartSurveillanceCam2DBScheduler();

        [OperationContract(IsOneWay = true)]
        void StopSurveillanceCam2DBScheduler();

        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeSurveillanceCam2DB();

    }
}