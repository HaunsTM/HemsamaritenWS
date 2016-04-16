namespace SurveillanceCam2DB.BLL.Interfaces
{
    using SurveillanceCam2DB.Model.Interfaces;

    public interface ICameraEventArgs
    {
        SurveillanceCam2DB.Model.Interfaces.IImage SnapShot { get; set; }

        System.Exception CameraException { get; set; }
    }
}