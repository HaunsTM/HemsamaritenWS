namespace SurveillanceCam2DB.BLL.Interfaces
{
    public interface ICameraEventArgs
    {
        System.Drawing.Image SnapShot { get; set; }

        System.Exception CameraException { get; set; }
    }
}