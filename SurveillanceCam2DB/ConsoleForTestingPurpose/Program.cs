//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace SurveillanceCam2DB.ConsoleForTestingPurpose
{
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Threading;

    using log4net;

    using SurveillanceCam2DB.BLL;
    using SurveillanceCam2DB.Model;
    using SurveillanceCam2DB.Model.Enums;

    using Action = SurveillanceCam2DB.Model.Action;

    internal class Program
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string DB_CONNECTION_STRING_NAME = "name=SurveillanceCamerasDBConnection";

        private static void Main(string[] args)
        {
            //TestScheduler();
            //TestConvertImagesInDbToFiles();
            //CreateDBTest();
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
        
        private static void TestScheduler()
        {
            JobScheduler jobScheduler;
            jobScheduler = new JobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
            Thread.Sleep(10000);
            jobScheduler.Stop();
            Thread.Sleep(10000);
            jobScheduler = new JobScheduler(DB_CONNECTION_STRING_NAME);
            jobScheduler.Start();
        }

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

