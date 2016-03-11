namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Net;

    using SurveillanceCam2DB.Model;

    public class ImageTransfer
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ImageTransfer(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition imagePosition, int storeImagesInThisQualityPercent, Size newImageSize, bool preserveImageAspectRatio, string dbConnectionStringName)
        {
            this.CamCurrent = camCurrent;
            this.ImagePosition = imagePosition;
            this.StoreImagesInThisQualityPercent = storeImagesInThisQualityPercent;
            this.NewImageSize = newImageSize;
            this.SurveillanceCam2DBContext = new SurveillanceCam2DBContext(dbConnectionStringName);
        }

        #region Properties

        public Model.Interfaces.ICamera CamCurrent { get; private set; }
        public Model.Interfaces.IPosition ImagePosition { get; private set; }
        public int StoreImagesInThisQualityPercent { get; private set; }
        public Size NewImageSize { get; private set; }
        public bool PreserveImageAspectRatio { get; private set; }
        public Model.SurveillanceCam2DBContext SurveillanceCam2DBContext { get; private set; }

        #endregion
        
        public bool DownloadImageFromCameraAndSaveItToDB()
        {
            var downloadAndSaveSucceeded = false;

            downloadAndSaveSucceeded = this.DownloadImageFromCameraAndSaveItToDB(
                                        camCurrent: this.CamCurrent,
                                        imagePosition: this.ImagePosition,
                                        storeImagesInThisQualityPercent: this.StoreImagesInThisQualityPercent, 
                                        surveillanceCam2DBContext: this.SurveillanceCam2DBContext,
                                        newImageSize: this.NewImageSize,
                                        preserveImageAspectRatio: this.PreserveImageAspectRatio);
            return downloadAndSaveSucceeded;
        }

        private bool DownloadImageFromCameraAndSaveItToDB(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition imagePosition, int storeImagesInThisQualityPercent, Size newImageSize, bool preserveImageAspectRatio, Model.SurveillanceCam2DBContext surveillanceCam2DBContext)
        {
            var downloadAndSaveSucceeded = false;

            try
            {
                var downloaded = this.DownloadImageFromCamera(
                    camCurrent: camCurrent,
                    imagePosition: imagePosition,
                    storeImagesInThisQualityPercent: storeImagesInThisQualityPercent,
                    newImageSize: newImageSize,
                    preserveImageAspectRatio: preserveImageAspectRatio);

                Model.Image newImage = (Model.Image)downloaded.Image;
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

        private ImageAndImageDataForDB DownloadImageFromCamera(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition imagePosition, int storeImagesInThisQualityPercent, Size newImageSize, bool preserveImageAspectRatio)
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
                    var surCamSnapshotImageFormat = new ImageFormat(surCamSnapshot.RawFormat.Guid);
                    var surCamSnapshotByteArray = imgConv.ImageToByteArray(
                        imageIn: surCamSnapshot,
                        format: surCamSnapshotImageFormat,
                        imageQualityPercent: storeImagesInThisQualityPercent,
                        newImageSize: newImageSize,
                        preserveImageAspectRatio: preserveImageAspectRatio);

                    var imageForDb = new Model.Image
                    {
                        Active = true,
                        Camera_Id = camCurrent.Id,
                        DataLength = surCamSnapshotByteArray.LongLength,
                        Description = "",
                        ImageFormat = surCamSnapshotImageFormat.ToString(),
                        Position_Id = imagePosition.Id,
                        SnapshotTime = surCamSnapshotTime,
                        ImageQualityPercent = storeImagesInThisQualityPercent,
                    };
                    
                    var imageDataForDb = new Model.ImageData
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

        private class ImageAndImageDataForDB
        {
            public ImageAndImageDataForDB(Model.Image image, Model.ImageData imageData)
            {
                this.Image = image;
                this.ImageData = imageData;
            }

            public Model.Image Image { get; private set; }
            public Model.ImageData ImageData { get; private set; }
        }

    }
}