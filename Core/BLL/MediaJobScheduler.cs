using Core.BLL.Interfaces;
using Core.Model.Interfaces;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Core.Model;
using log4net;

using Quartz;
using Quartz.Impl;

using IScheduler = Core.Model.Interfaces.IScheduler;

namespace Core.BLL
{
    public class MediaJobScheduler : IJobScheduler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        private Quartz.IScheduler Scheduler { get; set; }

        private NameValueCollection SchedulerProperties()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "Media_Scheduler";
            return properties;
        }

        public MediaJobScheduler(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;

            ISchedulerFactory sf = new StdSchedulerFactory(props: SchedulerProperties());
            this.Scheduler = sf.GetScheduler();
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
                log.Debug(String.Format("Started Scheduler for Media!"));
            }
            else
            {
                log.Warn(String.Format("Tried to start Scheduler for Media, but it was already started!"));
            }
        }

        public void Stop()
        {
            if (this.Scheduler.IsStarted)
            {
                this.Scheduler.Shutdown();
                log.Debug(String.Format("Shutdown Scheduler for Media!"));
            }
            else
            {
                log.Warn(String.Format("Tried to shutdown Scheduler for Media, but it was already shutdown!"));
            }
        }

        private List<JobDetailAndTrigger> PreparedJobs()
        {
            var jobDetailsAndTriggers = new List<JobDetailAndTrigger>();

            var mediaTasks = this.MediaTasks();

            foreach (var task in mediaTasks)
            {
                try
                {
                    var jobName = task.MediaSource != null ? $"Job for: {task.MediaSource.Name}" : $"Internal job for Hemsamariten";
                    var triggerName = task.MediaSource != null ? $"Trigger for: {task.MediaSource.Name}" : $"Trigger for ActionType: {task.MediaActionType.ActionTypeOption.ToString()}";
                    var jobAndTriggerGroup = "Cron expression: " + task.Scheduler.CronExpression;

                    var jobId = new JobKey(name: jobName, group: jobAndTriggerGroup);
                    var triggerId = new TriggerKey(name: triggerName, group: jobAndTriggerGroup);

                    //create a new Job

                    #region JobData
                    //PerformWork(IPlayer player, IMediaSource mediaSource, IMediaOutput mediaOutput, IMediaOutputVolume mediaOutputVolume, IMediaActionType mediaActionType)

                    var jsonSerializedCurrentActionId_Key = Newtonsoft.Json.JsonConvert.SerializeObject(task.Action.Id);
                    var jsonSerializedCurrentActionId_Value = "jsonSerializedCurrentActionId";

                    var jsonSerializedMediaSource_Key = "jsonSerializedMediaSource";
                    var jsonSerializedMediaSource_Value = Newtonsoft.Json.JsonConvert.SerializeObject(task.MediaSource); ;

                    var jsonSerializedMediaOutput_Key = "jsonSerializedMediaOutputTarget";
                    var jsonSerializedMediaOutput_Value = Newtonsoft.Json.JsonConvert.SerializeObject(task.MediaOutput); ;

                    var jsonSerializedMediaOutputVolume_Key = "jsonSerializedMediaOutputVolume";
                    var jsonSerializedMediaOutputVolume_Value = Newtonsoft.Json.JsonConvert.SerializeObject(task.MediaOutputVolume.Value);

                    var jsonSerializedMediaActionType_Key = "jsonSerializedMediaActionType";
                    var jsonSerializedMediaActionType_Value = Newtonsoft.Json.JsonConvert.SerializeObject(task.MediaActionType.ActionTypeOption.ToString());

                    var jsonDbConnectionStringName_Key = "jsonSerializedDbConnectionStringName";
                    var jsonDbConnectionStringName_Value = Newtonsoft.Json.JsonConvert.SerializeObject(this.DbConnectionStringName);


                    #endregion

                    var job = JobBuilder.Create<TellstickJob>()
                        .WithIdentity(jobId)
                        .UsingJobData(jsonSerializedCurrentActionId_Key, jsonSerializedCurrentActionId_Value)
                        .UsingJobData(jsonSerializedMediaSource_Key, jsonSerializedMediaSource_Value)
                        .UsingJobData(jsonSerializedMediaOutput_Key, jsonSerializedMediaOutput_Value)
                        .UsingJobData(jsonSerializedMediaOutputVolume_Key, jsonSerializedMediaOutputVolume_Value)
                        .UsingJobData(jsonSerializedMediaActionType_Key, jsonSerializedMediaActionType_Value)
                        .UsingJobData(jsonDbConnectionStringName_Key, jsonDbConnectionStringName_Value)
                        .Build();

                    //create a trigger for the corresponding Job
                    var actionCronExpression = task.Scheduler.CronExpression;

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
        
        /// <summary>
        /// Fetches a list of active tellstick job
        /// </summary>
        /// <returns>A list of stuff to do ()</returns>
        private List<RegisteredAction> MediaTasks()
        {
            var tellstickUnitsWithActions = new List<RegisteredAction>();

            try
            {
                using (var db = new Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
                {
                    var queryResult = from activeAction in db.Actions.OfType<MediaAction>()
                        where (activeAction.Active == true) &&
                              (activeAction.Scheduler != null ? activeAction.Scheduler.Active == true : false) &&

                              (activeAction.MediaSource != null ? activeAction.MediaSource.Active == true : true) &&
                              (activeAction.MediaOutput != null ? activeAction.MediaOutput.Active == true : true) &&
                              (activeAction.MediaOutputVolume != null
                                  ? activeAction.MediaOutputVolume.Active == true
                                  : true) &&
                              (activeAction.MediaActionType != null
                                  ? activeAction.MediaActionType.Active == true
                                  : true)
                        select
                            new RegisteredAction()
                            {

                                Scheduler = activeAction.Scheduler,
                                Action = activeAction,
                                MediaSource = activeAction.MediaSource,
                                MediaOutput = activeAction.MediaOutput,
                                MediaOutputVolume = activeAction.MediaOutputVolume,
                                MediaActionType = activeAction.MediaActionType
                            };



                tellstickUnitsWithActions = queryResult.ToList();
                }
            }
            catch (Exception ex)
            {

                log.Error("Could not retrieve actions for performance from database!", ex);
                throw ex;
            }
            return tellstickUnitsWithActions;
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

        private class RegisteredAction
        {
            public RegisteredAction() { }

            public IScheduler Scheduler { get; set; }
            public IMediaAction Action { get; set; }
            public IMediaSource MediaSource { get; set; }
            public IMediaOutput MediaOutput { get; set; }
            public IMediaOutputVolume MediaOutputVolume { get; set; }
            public IMediaActionType MediaActionType { get; set; }
        }
    }
}