namespace Tellstick.ConsoleForTestingPurpose
{
    using Tellstick.BLL;
    using Tellstick.Model;

    using System;
    using System.Drawing.Imaging;
    using System.Net;
    using Quartz;

    public class ImageTransfer
    {

        public ImageTransfer(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition camCurrentPosition, Uri camSnapshotUri, NetworkCredential camNetworkCredentials, int storeImagesInThisQualityPercent, Model.SurveillanceCam2DBContext surveillanceCam2DBContext)
        {
            CamCurrent = camCurrent;
            CamCurrentPosition = camCurrentPosition;
            CamSnapshotUri = camSnapshotUri;
            CamNetworkCredentials = camNetworkCredentials;
            StoreImagesInThisQualityPercent = storeImagesInThisQualityPercent;
            SurveillanceCam2DBContext = surveillanceCam2DBContext;
        }

        #region Properties

        public Model.Interfaces.ICamera CamCurrent { get; private set; }
        public Model.Interfaces.IPosition CamCurrentPosition { get; private set; }
        public Uri CamSnapshotUri { get; private set; }
        public NetworkCredential CamNetworkCredentials { get; private set; }
        public int StoreImagesInThisQualityPercent { get; private set; }
        public Model.SurveillanceCam2DBContext SurveillanceCam2DBContext { get; private set; }

        #endregion
        
        public bool DownloadImageFromCameraAndSaveItToDB()
        {
            var downloadAndSaveSucceeded = false;

            downloadAndSaveSucceeded = DownloadImageFromCameraAndSaveItToDB(
                                        camCurrent: CamCurrent,
                                        camCurrentPosition: CamCurrentPosition,
                                        camSnapshotUri: CamSnapshotUri,
                                        camNetworkCredentials: CamNetworkCredentials,
                                        storeImagesInThisQualityPercent: StoreImagesInThisQualityPercent, 
                                        surveillanceCam2DBContext: SurveillanceCam2DBContext);
            return downloadAndSaveSucceeded;
        }

        private bool DownloadImageFromCameraAndSaveItToDB(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition camCurrentPosition, Uri camSnapshotUri, NetworkCredential camNetworkCredentials, int storeImagesInThisQualityPercent, Model.SurveillanceCam2DBContext surveillanceCam2DBContext)
        {
            var downloadAndSaveSucceeded = false;

            try
            {
                var downloaded = DownloadImageFromCamera(
                    camCurrent: camCurrent,
                    camCurrentPosition: camCurrentPosition,
                    camSnapshotUri: camSnapshotUri,
                    camNetworkCredentials: camNetworkCredentials,
                    storeImagesInThisQualityPercent: storeImagesInThisQualityPercent);

                surveillanceCam2DBContext.Images.Add(downloaded.Image);
                surveillanceCam2DBContext.ImageData.Add(downloaded.ImageData);

                surveillanceCam2DBContext.SaveChanges();

                downloadAndSaveSucceeded = true;
            }
            catch (Exception ex)
            {
                downloadAndSaveSucceeded = false;
            }

            return downloadAndSaveSucceeded;

        }

        private ImageAndImageDataForDB DownloadImageFromCamera(Model.Interfaces.ICamera camCurrent, Model.Interfaces.IPosition camCurrentPosition, Uri camSnapshotUri, NetworkCredential camNetworkCredentials, int storeImagesInThisQualityPercent)
        {

            var imgConv = new Tellstick.BLL.ImageConverter();

            var surCam = new SurveillanceCam(snapshotUri: camSnapshotUri,
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
                        imageQualityPercent: storeImagesInThisQualityPercent);

                    var imageForDb = new Model.Image
                    {
                        Active = true,
                        Camera = (Camera)camCurrent,
                        DataLength = surCamSnapshotByteArray.LongLength,
                        Description = "",
                        ImageFormat = surCamSnapshotImageFormat.ToString(),
                        Position = (Position)camCurrentPosition,
                        SnapshotTime = surCamSnapshotTime,
                        ImageQualityPercent = storeImagesInThisQualityPercent
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
                Image = image;
                ImageData = imageData;
            }

            public Model.Image Image { get; private set; }
            public Model.ImageData ImageData { get; private set; }
        }

    }
}