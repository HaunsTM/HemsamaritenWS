namespace SurveillanceCam2DB.BLL
{
    using BLL.Interfaces;

    public class CameraEventArgs : System.EventArgs, ICameraEventArgs
    {

        public System.Drawing.Image SnapShot { get; set; }

        public System.Exception CameraException { get; set; }

    }
}
