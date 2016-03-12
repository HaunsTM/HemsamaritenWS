//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Tellstick.ConsoleForTestingPurpose
{
    using Tellstick.BLL;

    using System;
    using System.Configuration;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Net;

    using log4net;
    

    internal class Program
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string DB_CONNECTION_STRING_NAME = "name=SurveillanceCamerasDBConnection";

        private static void Main(string[] args)
        {
             //new JobScheduler().Start();
            //TestConvertImagesInDbToFiles();
            CreateDBTest();
            //TestLogger();
            //TestLinq();
        }

        private static void TestConvertImagesInDbToFiles()
        {
            using (var db = new Model.SurveillanceCam2DBContext("name=SurveillanceCamerasDBConnection"))
            {
                var imgConv = new BLL.ImageConverter();
                var imagesandImageData = (from img in db.Images
                                          join imgData in db.ImageData on img.ImageData equals imgData
                                          select new { img, imgData }).ToList();
                            
                foreach (var imgsAndImgsData in imagesandImageData)
                {
                    var tempImg = imgConv.ByteArrayToImage(imgsAndImgsData.imgData.Data);
                    var tempImg1 = new Bitmap(tempImg.Width, tempImg.Height, tempImg.PixelFormat);
                    using (Graphics g = Graphics.FromImage(tempImg1))
                    {
                        // Draw the original bitmap onto the graphics of the new bitmap
                        g.DrawImage(tempImg, Point.Empty);
                        g.Flush();
                    }
                    tempImg1.Save("c:\\Users\\hansb\\Desktop\\WinService\\" + imgsAndImgsData.img.SnapshotTime.ToFileTimeUtc().ToString() + ".jpg", ImageFormat.Jpeg);
                }

            }


        }

        private static void CreateDBTest()
        {
            System.Data.Entity.Database.SetInitializer(new DefaultDataDbInitializer());
            var db = new Model.SurveillanceCam2DBContext("name=SurveillanceCamerasDBConnection");
#if DEBUG
            db.Database.Initialize(true);
#endif
           
            //do something random stupid to force seed
            var stupidValue = db.Cameras.Count();

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
        
        private static void TestLinq()
        {
            using (var db = new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME))
            {
                var camerasAndActions = (from cam in db.Cameras
                                         join act in db.Actions on cam equals act.Camera
                                         join actType in db.ActionTypes on act.ActionType equals actType
                                         where
                                             cam.Active == true && act.Active == true
                                             && actType.Type
                                             == Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                         select new { cam, act }).ToList();

                var actionsAndCameras = (from act in db.Actions
                                         join cam in db.Cameras on act.Camera equals cam
                                         join actType in db.ActionTypes on act.ActionType equals actType
                                         where cam.Active == true && act.Active == true && actType.Type
                                             == Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB
                                         select new { cam, act }).ToList();

                var imagesandImageData = (from img in db.Images
                                          join imgData in db.ImageData on img.ImageData equals imgData
                                          select new { img, imgData }).ToList();

            }
        }
    }
    
}

