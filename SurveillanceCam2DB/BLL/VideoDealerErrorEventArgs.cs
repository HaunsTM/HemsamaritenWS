namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Runtime.Serialization;

    using SurveillanceCam2DB.BLL.Interfaces;

    [DataContract]
    public class VideoDealerErrorEventArgs : EventArgs, IVideoDealerErrorEventArgs
    {
        public Exception VideoDealerException { get; set; }
    }
}