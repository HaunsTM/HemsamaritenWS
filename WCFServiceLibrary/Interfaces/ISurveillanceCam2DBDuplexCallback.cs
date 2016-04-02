namespace WCFServiceLibrary.Interfaces
{
    using System.ServiceModel;
    
    using SurveillanceCam2DB.BLL.Interfaces;

    public interface ISurveillanceCam2DBDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendOnBeginCreatingVideoFile(IVideoDealerStartUpEventArgs eventArgs);

        [OperationContract(IsOneWay = true)]
        void SendOnVideoFrameWritten(IVideoDealerProcessEventArgs eventArgs);

        [OperationContract(IsOneWay = true)]
        void SendOnVideoFileCreated(IVideoDealerFinishEventArgs eventArgs);

        [OperationContract(IsOneWay = true)]
        void SendOnVideoCreatorError(IVideoDealerErrorEventArgs eventArgs);
    }
}