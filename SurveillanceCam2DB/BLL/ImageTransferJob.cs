namespace Tellstick.BLL
{
    using System;
    using System.Drawing;

    using Quartz;

    using log4net;
    

    [DisallowConcurrentExecution]
    public class ImageTransferJob : IJob
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ImageTransferJob()
        {
        }

        public void Execute(IJobExecutionContext context)
        {;
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            log.Debug(String.Format("Executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));

            try
            {
                var jsonSerializedCurrentCamera = dataMap.GetString("jsonSerializedCurrentCamera");
                var camCurrent = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Camera>(jsonSerializedCurrentCamera);

                var jsonSerializedDefaultImageQualityPercent = dataMap.GetString("jsonSerializedDefaultImageQualityPercent");
                var defaultImageQualityPercent = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedDefaultImageQualityPercent);

                var jsonSerializedDefaultMaxImageSize = dataMap.GetString("jsonSerializedDefaultMaxImageSize");
                var defaultMaxImageSize = Newtonsoft.Json.JsonConvert.DeserializeObject<Size>(jsonSerializedDefaultMaxImageSize);

                var jsonSerializedPreserveImageAspectRatio = dataMap.GetString("jsonSerializedPreserveImageAspectRatio");
                var preserveImageAspectRatio = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(jsonSerializedPreserveImageAspectRatio);

                var jsonSerializedTomatkameraPosition = dataMap.GetString("jsonSerializedTomatkameraPosition");
                var tomatkameraPosition = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Position>(jsonSerializedTomatkameraPosition);

                var jsonSerializedDbConnectionStringName = dataMap.GetString("jsonSerializedDbConnectionStringName");
                var connectionStringName = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonSerializedDbConnectionStringName);

                var imageTransfer = new ImageTransfer(camCurrent: camCurrent, imagePosition: tomatkameraPosition, storeImagesInThisQualityPercent: defaultImageQualityPercent, newImageSize: defaultMaxImageSize, preserveImageAspectRatio: preserveImageAspectRatio, dbConnectionStringName: connectionStringName);

                var jobDone = imageTransfer.DownloadImageFromCameraAndSaveItToDB();
                log.Debug(String.Format("Success with Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));

            }
            catch (Exception ex)
            {
                log.Fatal(String.Format("Failed in executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group), ex);
            }
        }

    }
}
 