namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    using log4net;

    using Quartz;
    using Quartz.Impl;

    using SurveillanceCam2DB.BLL.Interfaces;
    using SurveillanceCam2DB.Model.Interfaces;

    public class JobScheduler : IJobScheduler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        private Quartz.IScheduler Scheduler { get; set; }

        private NameValueCollection SchedulerProperties()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "SurveillanceCam2DB_Scheduler";
            return properties;
        }

        private IPosition _tomatkameraPosition;

        public JobScheduler(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
            
            ISchedulerFactory sf = new StdSchedulerFactory(props: SchedulerProperties());
            this.Scheduler = sf.GetScheduler();

            _tomatkameraPosition = null;
        }

        /// <summary>
        /// Returns a list of all jobs which are currently running. Used for debugging.
        /// </summary>
        public List<string> CurrentlyExecutingJobsNames
        {
            get
            {
                var executingJobs = this.Scheduler.GetCurrentlyExecutingJobs();
                var executingJobsNames = new List<string>();

                foreach (var executingJob in executingJobs)
                {
                    executingJobsNames.Add(executingJob.JobDetail.Key.Name);
                }

                return executingJobsNames;

            }
        }

        public void Start()
        {
            if (!this.Scheduler.IsStarted)
            {
                this.Scheduler.Start();

                var preparedJobs = this.PreparedJobs();

                foreach (var preparedJob in preparedJobs)
                {
                    this.Scheduler.ScheduleJob(jobDetail: preparedJob.Job, trigger: preparedJob.Trigger);
                }

                log.Debug(String.Format("Started Scheduler for SurveillanceCam2DB!"));
            }
            else
            {
                log.Warn(String.Format("Tried to start Scheduler for SurveillanceCam2DB, but it was already started!"));
            }
        }

        public void Stop()
        {
            if (this.Scheduler.IsStarted)
            {
                this.Scheduler.Shutdown();
                log.Debug(String.Format("Shutdown Scheduler for SurveillanceCam2DB!"));
            }
            else
            {
                log.Warn(String.Format("Tried to shutdown Scheduler for SurveillanceCam2DB, but it was already shutdown!"));
            }
        }

        private List<JobDetailAndTrigger> PreparedJobs()
        {
            var jobDetailsAndTriggers = new List<JobDetailAndTrigger>();

            var camerasWithActions = this.CamerasWithActions();

            foreach (var task in camerasWithActions)
            {
                try
                {
                    var jobName = "Job for: " + task.Camera.Name;
                    var triggerName = "Trigger for: " + task.Camera.Name;
                    var jobAndTriggerGroup = "Cron expression: " + task.SchedulerToUse.CronExpression;

                    var jobId = new JobKey(name: jobName, group: jobAndTriggerGroup);
                    var triggerId = new TriggerKey(name: triggerName, group: jobAndTriggerGroup);

                    //create a new Job
                    
                    #region JobData

                    var jsonSerializedCurrentCamera = Newtonsoft.Json.JsonConvert.SerializeObject(task.Camera);
                    var jobDataCurrentCamera_Key = "jsonSerializedCurrentCamera";
                    var jobDataCurrentCamera_Value = jsonSerializedCurrentCamera;

                    var jsonSerializedCurrentActionType = Newtonsoft.Json.JsonConvert.SerializeObject(task.ActionTypeToBeRequested);
                    var jobDataCurrentActionType_Key = "jsonSerializedCurrentActionType";
                    var jobDataCurrentActionType_Value = jsonSerializedCurrentActionType;

                    var jsonSerializedTomatkameraPosition = Newtonsoft.Json.JsonConvert.SerializeObject(this.TomatkameraPosition);
                    var jobDataTomatkameraPosition_Key = "jsonSerializedTomatkameraPosition";
                    var jobDataTomatkameraPosition_Value = jsonSerializedTomatkameraPosition;

                    var jsonSerializedCurrentActionId = Newtonsoft.Json.JsonConvert.SerializeObject(task.ActionToBeRequested.Id);
                    var jsonSerializedCurrentActionId_Key = "jsonSerializedCurrentActionId";
                    var jsonSerializedCurrentActionId_Value = jsonSerializedCurrentActionId;

                    var jsonSerializedDbConnectionStringName = Newtonsoft.Json.JsonConvert.SerializeObject(this.DbConnectionStringName);
                    var jsonDbConnectionStringName_Key = "jsonSerializedDbConnectionStringName";
                    var jsonDbConnectionStringName_Value = jsonSerializedDbConnectionStringName;

                    #endregion

                    var job = JobBuilder.Create<ImageTransferJob>()
                        .WithIdentity(jobId)
                        .UsingJobData(jobDataCurrentCamera_Key, jobDataCurrentCamera_Value)
                        .UsingJobData(jobDataCurrentActionType_Key, jobDataCurrentActionType_Value)
                        .UsingJobData(jobDataTomatkameraPosition_Key, jobDataTomatkameraPosition_Value)
                        .UsingJobData(jsonSerializedCurrentActionId_Key, jsonSerializedCurrentActionId_Value)
                        .UsingJobData(jsonDbConnectionStringName_Key, jsonDbConnectionStringName_Value)
                        .Build();

                    //create a trigger for the corresponding Job
                    var actionCronExpression = task.SchedulerToUse.CronExpression;

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

        private IPosition TomatkameraPosition
        {
            get
            {
                if (_tomatkameraPosition == null)
                {
                    using (var db = new Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
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

        private List<CameraWithAction> CamerasWithActions()
        {
            var camerasWithActions = new List<CameraWithAction>();

            using (var db = new Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
            {
                try
                {
                    var queryResult = (from act in db.Actions
                                       where
                                           act.Active == true && act.Camera.Active == true
                                           && act.ActionType.Active == true && act.Scheduler.Active == true
                                       select
                                           new CameraWithAction {
                                           ActionToBeRequested = act,
                                           Camera = act.Camera,
                                           ActionTypeToBeRequested = act.ActionType,
                                           SchedulerToUse = act.Scheduler}).ToList();

                    foreach (var ca in queryResult)
                    {
                        camerasWithActions.Add(ca);
                    }
                }
                catch (Exception ex)
                {
                    log.Fatal(
                        "Error creating list of CamerasWithActions",
                        ex);
                    return null;
                }
                return camerasWithActions;
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
            public CameraWithAction() { }
            public CameraWithAction(ICamera camera, IAction actionToBeRequested, IActionType actionTypeToBeRequested, Model.Interfaces.IScheduler schedulerToUse)
            {
                Camera = camera;
                ActionTypeToBeRequested = actionTypeToBeRequested;
                ActionToBeRequested = actionToBeRequested;
                SchedulerToUse = schedulerToUse;
            }

            public ICamera Camera { get;  set; }
            public IAction ActionToBeRequested { get;  set; }
            public IActionType ActionTypeToBeRequested { get;  set; }
            public Model.Interfaces.IScheduler SchedulerToUse { get;  set; }
        }
    }
}