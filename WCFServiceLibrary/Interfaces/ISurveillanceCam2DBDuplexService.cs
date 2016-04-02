namespace WCFServiceLibrary.Interfaces
{
    using System;
    using System.ServiceModel;
    using System.Threading.Tasks;

    using AForge.Video.FFMPEG;

    [ServiceContract(CallbackContract = typeof(ISurveillanceCam2DBDuplexCallback))]
    public interface ISurveillanceCam2DBDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void StartSurveillanceCam2DBScheduler();

        [OperationContract(IsOneWay = true)]
        void StopSurveillanceCam2DBScheduler();

        [OperationContract(IsOneWay = true)]
        void CreateAndInitializeSurveillanceCam2DB();
        
        [OperationContract(IsOneWay = true)]
        void CreateVideo(
            DateTime startTime,
            DateTime endTime,
            VideoCodec codec,
            string outputFileName,
            int width,
            int height,
            int frameRateMs);
    }
}