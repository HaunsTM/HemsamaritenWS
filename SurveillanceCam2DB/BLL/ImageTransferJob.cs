namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Drawing;

    using Quartz;

    using log4net;

    using SurveillanceCam2DB.Model;
    using SurveillanceCam2DB.Model.Enums;
    using SurveillanceCam2DB.Model.Interfaces;

    [DisallowConcurrentExecution]
    public class ImageTransferJob : IJob
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public delegate void ImageTransferJobEventHandler(IJob imageTransferrer, CameraEventArgs e);
        public event ImageTransferJobEventHandler OnImageDownloadedAndSavedSucceeded;
        public event ImageTransferJobEventHandler OnException;

        public ImageTransferJob()
        {
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            log.Debug(String.Format("Executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));

            try
            {
                var jsonSerializedCurrentCamera = dataMap.GetString("jsonSerializedCurrentCamera");
                var currentCamera = Newtonsoft.Json.JsonConvert.DeserializeObject<Camera>(jsonSerializedCurrentCamera);

                var jsonSerializedCurrentActionType = dataMap.GetString("jsonSerializedCurrentActionType");
                var currentActionType = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionType>(jsonSerializedCurrentActionType);

                var jsonSerializedTomatkameraPosition = dataMap.GetString("jsonSerializedTomatkameraPosition");
                var tomatkameraPosition = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Position>(jsonSerializedTomatkameraPosition);

                var jsonSerializedCurrentActionId = dataMap.GetString("jsonSerializedCurrentActionId");
                var currentActionId = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedCurrentActionId);

                var jsonSerializedDbConnectionStringName = dataMap.GetString("jsonSerializedDbConnectionStringName");
                var connectionStringName = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonSerializedDbConnectionStringName);

                var workDone = PerformWork(currentCamera: currentCamera, currentPosition: tomatkameraPosition, currentActionId: currentActionId, currentActionType: currentActionType, dbConnectionStringName: connectionStringName);

                if (workDone)
                {
                    //if we reach this point we have succeeded in sending a message to Camera
                    var performedActionsDealer = new PerformedActionsDealer(connectionStringName);

                    var actionPerformedTime = DateTime.Now;

                    //let's register (to db) what we have done
                    performedActionsDealer.Register(currentActionId, actionPerformedTime);

                    log.Debug(String.Format("Success with Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));
                }

            }
            catch (Exception ex)
            {
                 log.Fatal(String.Format("Failed in executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group), ex);
            }
        }

        private bool PerformWork(ICamera currentCamera, IPosition currentPosition, int currentActionId, IActionType currentActionType, string dbConnectionStringName)
        {
            var workPerformed = false;
            
            switch (currentActionType.Name)
            {
                case ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB:
                    var imageTransfer = new ImageTransfer(camCurrent: currentCamera, imagePosition: currentPosition, dbConnectionStringName: dbConnectionStringName);

                    try
                    {
                        var downloadedImageAndImageDataForDB = imageTransfer.DownloadImageFromCameraAndSaveItToDB();

                        //did the download and db save went well? 
                        workPerformed = downloadedImageAndImageDataForDB != null;

                        if (workPerformed)
                        {
                            //The event will be null until an event handler is actually added to it.
                            if (this.OnImageDownloadedAndSavedSucceeded != null) //Watch out for NullReferenceException
                            {
                                CameraEventArgs cEA = new CameraEventArgs { SnapShot = downloadedImageAndImageDataForDB };
                                OnImageDownloadedAndSavedSucceeded(this, cEA);
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format("Couldn't either download a snapshot from camera [{0}], or save snapshot to db dbConnectionStringName = [{1}]", currentCamera.GetPicURL, dbConnectionStringName));
                        }
                    }
                    catch (Exception ex)
                    {
                        //The event will be null until an event handler is actually added to it.
                        if (this.OnException != null) //Watch out for NullReferenceException
                        {
                            CameraEventArgs cEA = new CameraEventArgs { CameraException = ex };
                            OnException(this, cEA);
                        }

                        throw ex;
                    }
                    
                    break;

                case ActionTypes.Dont_CopyImageFrom_SurveillanceCamera2DB:
                    workPerformed = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return workPerformed;
        }
    }
}
 