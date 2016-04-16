namespace SurveillanceCam2DB.BLL
{
    using BLL.Interfaces;

    public class CameraEventArgs : System.EventArgs, ICameraEventArgs
    {

        public SurveillanceCam2DB.Model.Interfaces.IImage SnapShot { get; set; }

        public System.Exception CameraException { get; set; }

    }
}
