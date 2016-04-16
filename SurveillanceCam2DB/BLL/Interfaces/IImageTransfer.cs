namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System.Drawing.Imaging;

    using SurveillanceCam2DB.Model.Interfaces;

    public interface IImageTransfer
    {
        ICamera CamCurrent { get; }

        IPosition ImagePosition { get; }

        Model.SurveillanceCam2DBContext SurveillanceCam2DBContext { get; }

        event ImageTransfer.CameraEventHandler OnImageDownloadAndSaveSucceeded;

        event ImageTransfer.CameraEventHandler OnException;

        SurveillanceCam2DB.Model.Interfaces.IImage DownloadImageFromCameraAndSaveItToDB();

        ImageFormat GetImageFormat(System.Drawing.Image imageIn);
    }
}