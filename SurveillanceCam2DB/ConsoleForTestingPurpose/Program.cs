//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SurveillanceCam2DB.ConsoleForTestingPurpose
{
    using System;
    using System.Drawing.Imaging;
    using System.Linq;

    using log4net;

    using SurveillanceCam2DB.BLL;
    using SurveillanceCam2DB.BLL.Interfaces;
    using SurveillanceCam2DB.Model.Enums;

    using Action = SurveillanceCam2DB.Model.Action;

    internal class Program
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string DB_CONNECTION_STRING_NAME = "name=SurveillanceCamerasDBConnection";

        private static void Main(string[] args)
        {
            //new JobScheduler(DB_CONNECTION_STRING_NAME).Start();
            //TestConvertImagesInDbToFiles();
            //CreateDBTest();
            TestCreatingVideo();
            //TestLogger();
            //TestLinq();
        }

        private static void TestConvertImagesInDbToFiles()
        {
            using (var db = new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME))
            {
                var imgConv = new BLL.ImageConverter();
                var imagesandImageData = (from img in db.Images
                                          join imgData in db.ImageData on img.ImageData equals imgData
                                          select new { img, imgData }).ToList();
                            
                foreach (var imgsAndImgsData in imagesandImageData)
                {
                    using (var image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(imgsAndImgsData.imgData.Data)))
                    {
                        image.Save("c:\\Users\\hansb\\Desktop\\WinService\\" + imgsAndImgsData.img.SnapshotTime.ToFileTimeUtc().ToString() + ".jpg", ImageFormat.Jpeg);  // Or Png
                    }
                }

            }


        }

        private static void CreateDBTest()
        {
            System.Data.Entity.Database.SetInitializer(new DefaultDataDbInitializer());
            using (var db = new Model.SurveillanceCam2DBContext("name=SurveillanceCamerasDBConnection"))
            {
                db.Database.Initialize(true);

                db.Actions.Add(new Action
                                   {
                                       Active = true,
                                       Camera = (from cam in db.Cameras
                                                 where cam.Name == "Samsung Galaxy S4, Skräphögen"
                                                 select cam).First(),
                                       ActionType = (from actType in db.ActionTypes
                                                     where actType.Name==ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                                     select actType).First(),
                                       Scheduler = (from sched in db.Schedulers
                                                    where sched.CronExpression == "0/2 * * * * ?"
                                                    select sched).First()
                                   });
                db.SaveChanges();
            }
                

        }

        public static void TestLogger()
        {
            log4net.GlobalContext.Properties["testProperty"] = "This is my test property information";

            log.Debug("Other Class - Debug logging");
            log.Info("Other Class - Info logging");
            log.Warn("Other Class - Warn logging");
            log.Error("Other Class - Error logging");
            log.Fatal("Other Class - Fatal logging");
        }

        public static void TestCreatingVideo()
        {
            var createdVideo = false;

            DateTime startTime = new DateTime(2016, 3, 1);
            DateTime endTime = DateTime.Now;
                IImageConverter imageConverter = new BLL.ImageConverter();
            string outputFileName = "c:\\Users\\hansb\\Desktop\\WinService\\"
                                    + DateTime.Now.ToFileTimeUtc().ToString() + ".mp4";
            int width = 412;//4128;
            int height = 310;//3096;
            int frameRateMs = 5;
            var vd = new VideoDealer(DB_CONNECTION_STRING_NAME);
            vd.BeginCreatingVideoFile += BeginCreatingVideoFile;
            vd.FrameWritten += FrameWritten;
            vd.VideoFileCreated += VideoFileCreated;
            vd.VideoCreatorError += VideoCreatorError;

            createdVideo = vd.CreateVideo(
                startTime: startTime,
                endTime: endTime,
                imageConverter: imageConverter,
                outputFileName: outputFileName,
                width: width,
                height: height,
                frameRateMs: frameRateMs);

            Console.WriteLine("Created video: " + createdVideo.ToString());
            Console.ReadLine();
        }
        

        private static void BeginCreatingVideoFile(object sender, IVideoDealerStartUpEventArgs e)
        { Console.WriteLine(String.Format("Begin creating a videofile {0}, width: {1} heigt: {2}, frames: {3}/s",e.OutputFileName, e.Width, e.Height, e.FrameRateMs));}
        private static void FrameWritten(object sender, IVideoDealerProcessEventArgs e)
        { Console.WriteLine(String.Format("Processirn Image_Id {0}, number in sequence: {1} (/{2}). Percent done: {3}%", e.Image_Id, e.CurrentImageNumberInSequence, e.TotalNumberOfImagesInSequence, e.PercentDone)); }
        private static void VideoFileCreated(object sender, IVideoDealerFinishEventArgs e)
        { Console.WriteLine(String.Format("Done creating a videofile {0}, width: {1} heigt: {2}, frames: {3}/s", e.OutputFileName, e.Width, e.Height, e.FrameRateMs)); }
        private static void VideoCreatorError(object sender, IVideoDealerErrorEventArgs e)
        { Console.WriteLine(String.Format("An error occurred: {0}\nInner exception: {1}", e.VideoDealerException.Message, e.VideoDealerException.InnerException.Message)); }

        private void TestLinq()
        {
            using (var db = new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME))
            {
                var camerasAndActions = (from cam in db.Cameras
                                         join act in db.Actions on cam equals act.Camera
                                         join actType in db.ActionTypes on act.ActionType equals actType
                                         where
                                             cam.Active == true && act.Active == true
                                             && actType.Name
                                             == Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                         select new { cam, act }).ToList();

                var actionsAndCameras = (from act in db.Actions
                                         join cam in db.Cameras on act.Camera equals cam
                                         join actType in db.ActionTypes on act.ActionType equals actType
                                         where cam.Active == true && act.Active == true && actType.Name
                                             == Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                         select new { cam, act }).ToList();

                var imagesandImageData = (from img in db.Images
                                          join imgData in db.ImageData on img.ImageData equals imgData
                                          select new { img, imgData }).ToList();

            }
        }
    }
}

