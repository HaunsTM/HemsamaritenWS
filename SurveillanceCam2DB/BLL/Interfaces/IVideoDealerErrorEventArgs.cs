namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System;

    public interface IVideoDealerErrorEventArgs
    {
        Exception VideoDealerException { get; set; }
    }
}