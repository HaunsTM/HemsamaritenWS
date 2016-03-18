namespace Tellstick.Model.Interfaces
{
    using System.Drawing;

    public interface ICamera : IEntity
    {
        string Name { get; set; }
        
        string CameraNetworkUser { get; set; }

        string CameraNetworkUserPassword { get; set; }

        string GetPicURL { get; set; }

        int DefaultImageQualityPercent { get; set; }

        int DefaultMaxImageWidth { get; set; }
        int DefaultMaxImageHeight { get; set; }

        bool PreserveImageAspectRatio { get; set; }
    }
}