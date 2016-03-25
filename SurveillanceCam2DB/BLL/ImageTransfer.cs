namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Net;

    using SurveillanceCam2DB.Model;
    using SurveillanceCam2DB.Model.Interfaces;
    
    using Image = SurveillanceCam2DB.Model.Image;

    public class ImageTransfer
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ImageTransfer(ICamera camCurrent, IPosition imagePosition, string dbConnectionStringName)
        {
            this.CamCurrent = camCurrent;
            this.ImagePosition = imagePosition;
            this.SurveillanceCam2DBContext = new SurveillanceCam2DBContext(dbConnectionStringName);
        }

        #region Properties

        public ICamera CamCurrent { get; private set; }
        public IPosition ImagePosition { get; private set; }
        public Model.SurveillanceCam2DBContext SurveillanceCam2DBContext { get; private set; }

        #endregion
        
        public bool DownloadImageFromCameraAndSaveItToDB()
        {
            var downloadAndSaveSucceeded = false;

            downloadAndSaveSucceeded = this.DownloadImageFromCameraAndSaveItToDB(
                                        camCurrent: this.CamCurrent,
                                        imagePosition: this.ImagePosition,
                                        storeImagesInThisQualityPercent: this.CamCurrent.DefaultImageQualityPercent, 
                                        surveillanceCam2DBContext: this.SurveillanceCam2DBContext,
                                        newImageSize: new Size(this.CamCurrent.DefaultMaxImageWidth, this.CamCurrent.DefaultMaxImageHeight), 
                                        preserveImageAspectRatio: this.CamCurrent.PreserveImageAspectRatio);
            return downloadAndSaveSucceeded;
        }

        private bool DownloadImageFromCameraAndSaveItToDB(ICamera camCurrent, IPosition imagePosition, int storeImagesInThisQualityPercent, Size newImageSize, bool preserveImageAspectRatio, Model.SurveillanceCam2DBContext surveillanceCam2DBContext)
        {
            var downloadAndSaveSucceeded = false;

            try
            {
                var downloaded = this.DownloadImageFromCamera(
                    camCurrent: camCurrent,
                    imagePosition: imagePosition);

                Image newImage = (Image)downloaded.Image;
                var newImageData = downloaded.ImageData;
                surveillanceCam2DBContext.Images.Add(newImage);
                surveillanceCam2DBContext.ImageData.Add(downloaded.ImageData);

                surveillanceCam2DBContext.SaveChanges();

                downloadAndSaveSucceeded = true;
            }
            catch (NullReferenceException nrex)
            {
                log.Error("Is possibly surveillance camera turned off? Error downloading image from [" + camCurrent.Name + "] and saving it to db [" + surveillanceCam2DBContext.Database.Connection.ConnectionString + "]", nrex);
                throw nrex;
            }
            catch (Exception ex)
            { 
                log.Error("Error downloading image from ["+camCurrent.Name+"] and saving it to db ["+ surveillanceCam2DBContext.Database.Connection.ConnectionString + "]",ex);
                throw ex;
            }

            return downloadAndSaveSucceeded;

        }

        private ImageAndImageDataForDB DownloadImageFromCamera(ICamera camCurrent, IPosition imagePosition)
        {

            var imgConv = new SurveillanceCam2DB.BLL.ImageConverter();
            var picUrl = camCurrent.GetPicURL;
            var camNetworkCredentials = new NetworkCredential(userName: camCurrent.CameraNetworkUser, password: camCurrent.CameraNetworkUserPassword);
            var surCam = new SurveillanceCam(snapshotUri: new Uri(picUrl),
                                             networkCredentials: camNetworkCredentials);

            //get a snapshot
            var surCamSnapshot = surCam.DownloadRemoteImage();

            //did we get anything?
            if (surCamSnapshot != null)
            {
                var surCamSnapshotTime = DateTime.Now;
                var surCamSnapshotImageFormat = GetImageFormat(surCamSnapshot); //new ImageFormat(surCamSnapshot.RawFormat.Guid);
                var newImageSize = new Size(
                    width: camCurrent.DefaultMaxImageWidth,
                    height: camCurrent.DefaultMaxImageHeight);
                var surCamSnapshotByteArray = imgConv.ImageToByteArray(
                    imageIn: surCamSnapshot,
                    format: surCamSnapshotImageFormat,
                    imageQualityPercent: camCurrent.DefaultImageQualityPercent,
                    newImageSize: newImageSize,
                    preserveImageAspectRatio: camCurrent.PreserveImageAspectRatio);

                var imageForDb = new Image
                {
                    Active = true,
                    Camera_Id = camCurrent.Id,
                    DataLength = surCamSnapshotByteArray.LongLength,
                    Description = "",
                    ImageFormat = surCamSnapshotImageFormat.ToString(),
                    Position_Id = imagePosition.Id,
                    SnapshotTime = surCamSnapshotTime,
                    ImageQualityPercent = camCurrent.DefaultImageQualityPercent,
                };
                    
                var imageDataForDb = new ImageData
                {
                    Active = true,
                    Data = surCamSnapshotByteArray,
                    Image = imageForDb
                };

                var retVal = new ImageAndImageDataForDB(imageForDb, imageDataForDb);

                return retVal;
            }
            else
            {
                //No, we didn't got any image!
                return null;
            }
        }

        public ImageFormat GetImageFormat(System.Drawing.Image imageIn)
        {
            ImageFormat format = null;

            if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                format = System.Drawing.Imaging.ImageFormat.Bmp;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
            {
                format = System.Drawing.Imaging.ImageFormat.Emf;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
            {
                format = System.Drawing.Imaging.ImageFormat.Exif;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {
                format = System.Drawing.Imaging.ImageFormat.Gif;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
            {
                format = System.Drawing.Imaging.ImageFormat.Icon;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                format = System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
            {
                format = System.Drawing.Imaging.ImageFormat.MemoryBmp;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                format = System.Drawing.Imaging.ImageFormat.Png;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
            {
                format = System.Drawing.Imaging.ImageFormat.Tiff;
            }
            else if (imageIn.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
            {
                format = System.Drawing.Imaging.ImageFormat.Wmf;
            }
            else
            {
                throw new BadImageFormatException("Unrecognized image format!");
            }

            return format;
        }

        private class ImageAndImageDataForDB
        {
            public ImageAndImageDataForDB(Image image, ImageData imageData)
            {
                this.Image = image;
                this.ImageData = imageData;
            }

            public Image Image { get; private set; }
            public ImageData ImageData { get; private set; }
        }

    }
}