namespace Tellstick.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Linq;

    using log4net;

    using Quartz;
    using Quartz.Impl;

    using Tellstick.Model;

    public class JobScheduler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const string DB_CONNECTION_STRING_NAME = "name=SurveillanceCamerasDBConnection";

        private Model.Interfaces.IPosition _tomatkameraPosition;

        public JobScheduler()
        {
            _tomatkameraPosition = null;
        }

        public void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            var preparedJobs = this.PreparedJobs();

            foreach (var preparedJob in preparedJobs)
            {
                scheduler.ScheduleJob(jobDetail: preparedJob.Job, trigger: preparedJob.Trigger);
            }
        }

        private List<JobDetailAndTrigger> PreparedJobs()
        {
            var jobDetailsAndTriggers = new List<JobDetailAndTrigger>();

            var camerasWithActions = this.CamerasWithActions(Model.Enums.ActionTypes.Do_CopyImageFrom_SurveillanceCamera2DB);

            foreach (var cameraAndAction in camerasWithActions)
            {
                try
                {
                    var currentCamera = (Camera)cameraAndAction.Camera;
                    var currentAction = cameraAndAction.ActionToBeRequested;

                    var jobName = "Job for: " + currentCamera.Name;
                    var triggerName = "Trigger for: " + currentCamera.Name;
                    var jobAndTriggerGroup = "Cron expression: " + currentAction.CronExpression;

                    var jobId = new JobKey(name: jobName, group: jobAndTriggerGroup);
                    var triggerId = new TriggerKey(name: triggerName, group: jobAndTriggerGroup);

                    //create a new Job
                    
                    #region JobData

                    var jsonSerializedCurrentCamera = Newtonsoft.Json.JsonConvert.SerializeObject(currentCamera);
                    var jobDataCurrentCamera_Key = "jsonSerializedCurrentCamera";
                    var jobDataCurrentCamera_Value = jsonSerializedCurrentCamera;

                    var jsonSerializedDefaultImageQualityPercent = Newtonsoft.Json.JsonConvert.SerializeObject(currentCamera.DefaultImageQualityPercent);
                    var jsonSerializedDefaultImageQualityPercent_Key = "jsonSerializedDefaultImageQualityPercent";
                    var jsonSerializedDefaultImageQualityPercent_Value = jsonSerializedDefaultImageQualityPercent;

                    var jsonSerializedDefaultMaxImageSize = Newtonsoft.Json.JsonConvert.SerializeObject(new Size(width: currentCamera.DefaultMaxImageWidth, height: currentCamera.DefaultMaxImageHeight));
                    var jsonSerializedDefaultMaxImageSize_Key = "jsonSerializedDefaultMaxImageSize";
                    var jsonSerializedDefaultMaxImageSize_Value = jsonSerializedDefaultMaxImageSize;

                    var jsonSerializedPreserveImageAspectRatio = Newtonsoft.Json.JsonConvert.SerializeObject(currentCamera.PreserveImageAspectRatio);
                    var jsonSerializedPreserveImageAspectRatio_Key = "jsonSerializedPreserveImageAspectRatio";
                    var jsonSerializedPreserveImageAspectRatio_Value = jsonSerializedPreserveImageAspectRatio;


                    var jsonSerializedTomatkameraPosition = Newtonsoft.Json.JsonConvert.SerializeObject(this.TomatkameraPosition);
                    var jobDataTomatkameraPosition_Key = "jsonSerializedTomatkameraPosition";
                    var jobDataTomatkameraPosition_Value = jsonSerializedTomatkameraPosition;

                    var jsonSerializedDbConnectionStringName = Newtonsoft.Json.JsonConvert.SerializeObject(DB_CONNECTION_STRING_NAME);
                    var jsonDbConnectionStringName_Key = "jsonSerializedDbConnectionStringName";
                    var jsonDbConnectionStringName_Value = jsonSerializedDbConnectionStringName;

                    #endregion

                    var job = JobBuilder.Create<ImageTransferJob>()
                        .WithIdentity(jobId)
                        .UsingJobData(jobDataCurrentCamera_Key, jobDataCurrentCamera_Value)
                        .UsingJobData(jsonSerializedDefaultImageQualityPercent_Key, jsonSerializedDefaultImageQualityPercent_Value)
                        .UsingJobData(jsonSerializedDefaultMaxImageSize_Key, jsonSerializedDefaultMaxImageSize_Value)
                        .UsingJobData(jsonSerializedPreserveImageAspectRatio_Key, jsonSerializedPreserveImageAspectRatio_Value)
                        .UsingJobData(jobDataTomatkameraPosition_Key, jobDataTomatkameraPosition_Value)
                        .UsingJobData(jsonDbConnectionStringName_Key, jsonDbConnectionStringName_Value)
                        .Build();

                    //create a trigger for the corresponding Job
                    var actionCronExpression = currentAction.CronExpression;

                    var trigger = TriggerBuilder.Create()
                        .WithIdentity(triggerId)
                        .WithCronSchedule(actionCronExpression)
                        .ForJob(jobId)
                        .Build();

                    //create a JobDetailAndTrigger-object from the Job with corresponding Trigger
                    var jobDetailAndTrigger = new JobDetailAndTrigger(job: job, trigger: trigger);

                    //add JobDetailAndTrigger-object to jobDetailsAndTriggers-List<JobDetailAndTrigger>
                    jobDetailsAndTriggers.Add(jobDetailAndTrigger);
                }
                catch (Exception ex)
                {
                    log.Fatal("Error creating list of IJobDetail(s) with ITrigger(s).", ex);
                    throw ex;
                }
            }
            return jobDetailsAndTriggers;
        }

        private Model.Interfaces.IPosition TomatkameraPosition
        {
            get
            {
                if (_tomatkameraPosition == null)
                {
                    using (var db = new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME))
                    {
                        try
                        {
                            var tomatkameraPosition = (from pos in db.Positions
                                                       where pos.Description == "Tomatkamera"
                                                       select pos).First();

                            _tomatkameraPosition = tomatkameraPosition;
                        }
                        catch (Exception ex)
                        {
                            log.Error("Couldn't find an Model.Position-entity for Position.Description == Tomatkamera", ex);
                            throw ex;
                        }
                        

                    }
                }

               return _tomatkameraPosition;
            }
        }

        private List<CameraWithAction> CamerasWithActions(Model.Enums.ActionTypes actionType)
        {
            var camerasWithActions = new List<CameraWithAction>();

            using (var db = new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME))
            {
                try
                {
                    var camerasAndActions = (from cam in db.Cameras
                                            join act in db.Actions on cam equals act.Camera
                                            join actType in db.ActionTypes on act.ActionType equals actType
                                            where
                                                cam.Active == true && act.Active == true
                                                && actType.Type == actionType
                                            select new{cam, act}).ToList();
                    foreach (var ca in camerasAndActions)
                    {
                        camerasWithActions.Add(new CameraWithAction(camera: (Model.Camera)ca.cam, actionToBeRequested: (Model.Action)ca.act));
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal(
                        "Error creating list of ICamera(s) and their " + actionType.ToString("G") + " Action(s)",
                        ex);
                    return null;
                }
                return camerasWithActions;
            }
        }

        public Model.SurveillanceCam2DBContext SurveillanceCam2DBContext
        {
            get
            {
                return new Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME);
            }
        }
        
        private class JobDetailAndTrigger
        {
            public JobDetailAndTrigger(IJobDetail job, ITrigger trigger)
            {
                Job = job;
                Trigger = trigger;
            }
            public IJobDetail Job { get; private set; }
            public ITrigger Trigger { get; private set; }
        }

        private class CameraWithAction
        {
            public CameraWithAction(Model.Interfaces.ICamera camera, Model.Interfaces.IAction actionToBeRequested)
            {
                Camera = camera;
                ActionToBeRequested = actionToBeRequested;
            }

            public Model.Interfaces.ICamera Camera { get; private set; }
            public Model.Interfaces.IAction ActionToBeRequested { get; private set; }
        }
    }
}