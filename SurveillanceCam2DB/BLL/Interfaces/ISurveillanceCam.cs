namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System;
    using System.Drawing;
    using System.Net;
    
    public interface ISurveillanceCam
    {
        Uri SnapshotUri { get; }
        NetworkCredential NetworkCredentials { get; }

        /// <summary>
        /// Downloads an remote image. The Image Uri should be set in the (this.) constructor.
        /// </summary>
        /// <returns></returns>
        Image DownloadRemoteImage();

        /// <summary>
        /// Downloads a remote image
        /// </summary>
        /// <param name="snapshotUri">Resource Uri, <example>new Uri("http://10.11.12.13:1415/photo.jpg")</example></param>
        /// <returns></returns>
        Image DownloadRemoteImage(Uri snapshotUri);
        
    }
}