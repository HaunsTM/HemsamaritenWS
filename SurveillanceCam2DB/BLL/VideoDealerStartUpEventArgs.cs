namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Runtime.Serialization;

    using SurveillanceCam2DB.BLL.Interfaces;

    [DataContract]
    public class VideoDealerStartUpEventArgs : EventArgs, IVideoDealerStartUpEventArgs
    {
        public string OutputFileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRateMs { get; set; }
    }
}