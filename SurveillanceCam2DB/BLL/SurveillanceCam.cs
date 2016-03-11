namespace SurveillanceCam2DB.BLL
{
    using BLL.Interfaces;

    using System;
    using System.Drawing;
    using System.Net;
    using log4net;

    public class SurveillanceCam : ISurveillanceCam
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructors

        public SurveillanceCam()
        {
        }

        public SurveillanceCam(Uri snapshotUri)
        {
            this.SnapshotUri = snapshotUri;
        }


        public SurveillanceCam(NetworkCredential networkCredentials)
        {
            this.NetworkCredentials = networkCredentials;
        }

        public SurveillanceCam(Uri snapshotUri, NetworkCredential networkCredentials)
        {
            this.SnapshotUri = snapshotUri;
            this.NetworkCredentials = networkCredentials;
        }

        #endregion

        public NetworkCredential NetworkCredentials { get; private set; }

        public Uri SnapshotUri { get; private set; }

        /// <summary>
        /// Downloads an remote image. The Image Uri should be set in (this) constructor.
        /// </summary>
        /// <returns></returns>
        public Image DownloadRemoteImage()
        {
            return DownloadRemoteImage(SnapshotUri);
        }

        /// <summary>
        /// Downloads a remote image
        /// </summary>
        /// <param name="snapshotUri">Resource Uri, <example>new Uri("http://10.11.12.13:1415/photo.jpg")</example></param>
        /// <returns></returns>
        public Image DownloadRemoteImage(Uri snapshotUri)
        {
            Image returnImage = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(snapshotUri);
                request.Credentials = this.NetworkCredentials;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // Check that the remote file was found. The ContentType check is performed since a request for a non-existent
                    // image file might be redirected to a 404-page, which would yield the StatusCode "OK", even though the image was not
                    // found.
                    if ((response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Moved
                         || response.StatusCode == HttpStatusCode.Redirect)
                        && response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                    {
                        // if the remote file was found, download it
                        using (var inputStream = response.GetResponseStream())
                        {
                            if (inputStream != null)
                            {
                                returnImage = Image.FromStream(inputStream);
                                inputStream.Close();
                            }
                        }
                    }
                    else
                    {
                        throw new WebException(
                            "Error finding file " + snapshotUri + ". HttpStatusCode: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception message: " + ex.Message, ex);
                throw ex;
            }
            return returnImage;
        }
    }
}
