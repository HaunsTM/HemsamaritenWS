namespace SurveillanceCam2DB.BLL
{
    using System.Runtime.Serialization;

    using SurveillanceCam2DB.BLL.Interfaces;

    [DataContract]
    public class VideoDealerFinishEventArgs : VideoDealerProcessEventArgs, IVideoDealerFinishEventArgs
    {
        public string OutputFileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRateMs { get; set; }
    }
}