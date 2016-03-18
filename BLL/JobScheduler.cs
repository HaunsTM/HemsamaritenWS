namespace Tellstick.BLL
{
    using Tellstick.Model.Interfaces;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using Quartz;
    using Quartz.Impl;

    public class JobScheduler
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        private IScheduler Scheduler { get; set; }

        public JobScheduler(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
            this.Scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }


        public void Start()
        {

            this.Scheduler.Start();

            var preparedJobs = this.PreparedJobs();

            foreach (var preparedJob in preparedJobs)
            {
                this.Scheduler.ScheduleJob(jobDetail: preparedJob.Job, trigger: preparedJob.Trigger);
            }
        }

        public void Stop()
        {
            this.Scheduler.Shutdown();
        }

        private List<JobDetailAndTrigger> PreparedJobs()
        {
            var jobDetailsAndTriggers = new List<JobDetailAndTrigger>();

            var tellstickUnitsWithActions = this.TellstickUnitsWithActions();

            foreach (var task in tellstickUnitsWithActions)
            {
                try
                {
                    var jobName = "Job for: " + task.Unit.Name;
                    var triggerName = "Trigger for: " + task.Unit.Name;
                    var jobAndTriggerGroup = "Cron expression: " + task.Scheduler.CronExpression;

                    var jobId = new JobKey(name: jobName, group: jobAndTriggerGroup);
                    var triggerId = new TriggerKey(name: triggerName, group: jobAndTriggerGroup);

                    //create a new Job
                    
                    #region JobData

                    var jsonSerializedTellstickActionType = Newtonsoft.Json.JsonConvert.SerializeObject(task.ActionType.Type);
                    var jsonSerializedTellstickActionType_Key = "jsonSerializedTellstickActionType";
                    var jsonSerializedTellstickActionType_Value = jsonSerializedTellstickActionType;

                    var jsonSerializedCurrentNativeDeviceId = Newtonsoft.Json.JsonConvert.SerializeObject(task.Unit.NativeDeviceId);
                    var jsonSerializedCurrentNativeDeviceId_Key = "jsonSerializedCurrentNativeDeviceId";
                    var jsonSerializedCurrentNativeDeviceId_Value = jsonSerializedCurrentNativeDeviceId;


                    var jsonSerializedCurrentDimValue = Newtonsoft.Json.JsonConvert.SerializeObject(task.ActionType.DimValue);
                    var jsonSerializedCurrentDimValue_Key = "jsonSerializedCurrentDimValue";
                    var jsonSerializedCurrentDimValue_Value = jsonSerializedCurrentDimValue;

                    var jsonSerializedCurrentActionId= Newtonsoft.Json.JsonConvert.SerializeObject(task.Action.Id);
                    var jsonSerializedCurrentActionId_Key = "jsonSerializedCurrentActionId";
                    var jsonSerializedCurrentActionId_Value = jsonSerializedCurrentActionId;

                    var jsonSerializedDbConnectionStringName = Newtonsoft.Json.JsonConvert.SerializeObject(this.DbConnectionStringName);
                    var jsonDbConnectionStringName_Key = "jsonSerializedDbConnectionStringName";
                    var jsonDbConnectionStringName_Value = jsonSerializedDbConnectionStringName;

                    #endregion

                    var job = JobBuilder.Create<TellstickJob>()
                        .WithIdentity(jobId)
                        .UsingJobData(jsonSerializedTellstickActionType_Key, jsonSerializedTellstickActionType_Value)
                        .UsingJobData(jsonSerializedCurrentNativeDeviceId_Key, jsonSerializedCurrentNativeDeviceId_Value)
                        .UsingJobData(jsonSerializedCurrentDimValue_Key, jsonSerializedCurrentDimValue_Value)
                        .UsingJobData(jsonSerializedCurrentActionId_Key, jsonSerializedCurrentActionId_Value)
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
        private List<TellstickUnitWithAction> TellstickUnitsWithActions()
        {
            var tellstickUnitsWithActions = new List<TellstickUnitWithAction>();

            try
            {
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    var queryResult = from activeAction in db.TellstickActions
                                      where activeAction.Active == true &&
                                            activeAction.TellstickScheduler.Active == true &&
                                            activeAction.TellstickActionType.Active == true &&
                                            activeAction.TellstickUnit.Active == true &&
                                            activeAction.TellstickUnit.TellstickProtocol.Active == true &&
                                            activeAction.TellstickUnit.TellstickParameter.Active == true
                                      select
                                          new TellstickUnitWithAction
                                          {
                                              Scheduler = activeAction.TellstickScheduler,
                                              Action = activeAction,
                                              ActionType = activeAction.TellstickActionType,
                                              Unit = activeAction.TellstickUnit
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

        private class TellstickUnitWithAction
        {
            public TellstickUnitWithAction() { }
            public TellstickUnitWithAction(ITellstickScheduler currentScheduler, ITellstickAction currentAction, ITellstickActionType currentActionType, ITellstickUnit currentUnit)
            {
                this.Scheduler = currentScheduler;
                this.Action = currentAction;
                this.ActionType = currentActionType;
                this.Unit = currentUnit;
            }

            public ITellstickScheduler Scheduler { get; set; }
            public ITellstickAction Action { get; set; }
            public ITellstickActionType ActionType { get; set; }
            public ITellstickUnit Unit { get; set; }
        }
    }
}