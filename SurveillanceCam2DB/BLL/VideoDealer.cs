namespace SurveillanceCam2DB.BLL
{
    using SurveillanceCam2DB.BLL.Interfaces;

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;

    using AForge.Video.FFMPEG;

    public class VideoDealer
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region EVENTS
        public delegate void VideoDealerStartUpEventHandler(VideoDealer checker, IVideoDealerStartUpEventArgs e);
        public delegate void VideoDealerProcessEventHandler(VideoDealer checker, IVideoDealerProcessEventArgs e);
        public delegate void VideoDealerFinishEventHandler(VideoDealer checker, IVideoDealerFinishEventArgs e);
        public delegate void VideoDealerErrorEventHandler(VideoDealer checker, IVideoDealerErrorEventArgs e);
        public event VideoDealerStartUpEventHandler BeginCreatingVideoFile;
        public event VideoDealerProcessEventHandler FrameWritten;
        public event VideoDealerFinishEventHandler VideoFileCreated;
        public event VideoDealerErrorEventHandler VideoCreatorError;
        #endregion

        public string DbConnectionStringName { get; private set; }

        public VideoDealer(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
        }

        public bool CreateVideo(DateTime startTime, DateTime endTime, IImageConverter imageConverter, string outputFileName, int width, int height, int frameRateMs)
        {
            var videoCreated = false;
            try
            {
                log.Debug(String.Format("Begin creating video [{0}] from {1} to {2}", outputFileName, startTime.ToString(), endTime.ToString()));
                var imageSequence = this.ImagesAndImagesData(imageSequenceStart: startTime, imageSequenceEnd: endTime);
                videoCreated = this.CreateVideo(
                    imagesAndImagesData: imageSequence,
                    imageConverter: imageConverter,
                    outputFileName: outputFileName,
                    codec: VideoCodec.MPEG4, 
                    width: width,
                    height: height,
                    frameRateMs: frameRateMs);
                videoCreated = true;
                log.Debug(String.Format("Success creating video [{0}] from {1} to {2}", outputFileName, startTime.ToString(), endTime.ToString()));
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed creating video [{0}] from {1} to {2}", outputFileName, startTime.ToString(), endTime.ToString()), ex);

                //raise the event
                this.VideoCreatorError?.Invoke(checker: this, e: new VideoDealerErrorEventArgs {VideoDealerException = ex});
                
                throw ex;
            }
            

            return videoCreated;
        }

        private bool CreateVideo(List<ImageAndImageData> imagesAndImagesData, IImageConverter imageConverter, string outputFileName, AForge.Video.FFMPEG.VideoCodec codec, int width, int height, int frameRateMs)
        {
            var videoCreated = false;
            var sizeToBeUsed = new Size(width: width, height: height);

            var imageNumber = 0;
            var totalNumberOfImagesInSequence = imagesAndImagesData.Count;

            // create instance of video writer
            using (var vFWriter = new AForge.Video.FFMPEG.VideoFileWriter())
            {
                // create new video file
                vFWriter.Open(outputFileName, width, height, frameRateMs, codec);
                //raise the event
                this.BeginCreatingVideoFile?.Invoke(
                    checker: this,
                    e:
                        new VideoDealerStartUpEventArgs
                        {
                            OutputFileName = outputFileName,
                            Width = width,
                            Height = height,
                            FrameRateMs = frameRateMs
                        });

                //loop throught all images in the collection
                foreach (var imageEntity in imagesAndImagesData)
                {
                    imageNumber++;

                    //what's the current image data?
                    var imageByteArray = imageEntity.ImageData.Data;
                    var bmp = imageConverter.ByteArrayToBitmap(imageByteArray, sizeToBeUsed);

                    vFWriter.WriteVideoFrame(bmp);
                    
                    //raise the event
                    this.FrameWritten?.Invoke(checker: this, 
                                              e: new VideoDealerProcessEventArgs {
                                                 Image_Id = imageEntity.Image.Id,
                                                 CurrentImageNumberInSequence = imageNumber,
                                                 TotalNumberOfImagesInSequence = totalNumberOfImagesInSequence });
                }
                vFWriter.Close();
                videoCreated = true;

                //raise the event
                this.VideoFileCreated?.Invoke(
                    checker: this,
                    e:
                        new VideoDealerFinishEventArgs
                            {
                                OutputFileName = outputFileName,
                                Width = width,
                                Height = height,
                                FrameRateMs = frameRateMs
                            });

            }
            return videoCreated;
        }

        private List<ImageAndImageData> ImagesAndImagesData(DateTime imageSequenceStart, DateTime imageSequenceEnd)
        {
            var imagesAndImagesData = new List<ImageAndImageData>();

            using (var db = new Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
            {
                var queryResult = from img in db.Images
                                  join imgData in db.ImageData on img.ImageData equals imgData
                                  where img.SnapshotTime >= imageSequenceStart && img.SnapshotTime <= imageSequenceEnd
                                  select new ImageAndImageData { Image = img, ImageData = imgData};

                foreach (var imageAndImageData in queryResult)
                {
                    imagesAndImagesData.Add(imageAndImageData);
                }
            }
            return imagesAndImagesData;
        }

        private class ImageAndImageData
        {
            public ImageAndImageData() { }
            public ImageAndImageData(Model.Interfaces.IImage image, Model.Interfaces.IImageData imageData)
            {
                Image = image;
                ImageData = imageData;
            }

            public Model.Interfaces.IImage Image { get; set; }
            public Model.Interfaces.IImageData ImageData { get; set; }
        }
    }

    public interface IVideoDealerStartUpEventArgs
    {
        string OutputFileName { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int FrameRateMs { get; set; }
    }

    public class VideoDealerStartUpEventArgs : EventArgs, IVideoDealerStartUpEventArgs
    {
        public string OutputFileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRateMs { get; set; }
    }

    public interface IVideoDealerProcessEventArgs
    {
        int Image_Id { get; set; }

        int CurrentImageNumberInSequence { get; set; }

        int TotalNumberOfImagesInSequence { get; set; }
        int PercentDone { get; }
    }

    public class VideoDealerProcessEventArgs : EventArgs, IVideoDealerProcessEventArgs
    {
        public int Image_Id { get; set; }
        public int CurrentImageNumberInSequence { get; set; }
        public int TotalNumberOfImagesInSequence { get; set; }
        public int PercentDone
        {
            get
            {
                var donePercent = (CurrentImageNumberInSequence / TotalNumberOfImagesInSequence) * 100;
                return donePercent;
            }
        }
    }

    public interface IVideoDealerFinishEventArgs : IVideoDealerStartUpEventArgs { }

    public class VideoDealerFinishEventArgs : VideoDealerProcessEventArgs, IVideoDealerFinishEventArgs
    {
        public string OutputFileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameRateMs { get; set; }
    }

    public interface IVideoDealerErrorEventArgs
    {
        Exception VideoDealerException { get; set; }
    }

    public class VideoDealerErrorEventArgs : EventArgs, IVideoDealerErrorEventArgs
    {
        public Exception VideoDealerException { get; set; }
    }
}