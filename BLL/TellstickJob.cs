namespace Tellstick.BLL
{
    using System;

    using Quartz;

    using Tellstick.BLL.Interfaces;
    using Tellstick.Model.Enums;

    [DisallowConcurrentExecution]
    public class TellstickJob : IJob
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TellstickJob()
        {
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            log.Debug(String.Format("Executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));

            try
            {
                #region Job Data

                var jsonSerializedCurrentTellstickActionType = dataMap.GetString("jsonSerializedTellstickActionType");
                var currentTellstickActionType = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionTypeOption>(jsonSerializedCurrentTellstickActionType);

                var jsonSerializedCurrentNativeDeviceId = dataMap.GetString("jsonSerializedCurrentNativeDeviceId");
                var nativeDeviceId = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedCurrentNativeDeviceId);
                
                var jsonSerializedCurrentActionId = dataMap.GetString("jsonSerializedCurrentActionId");
                var currentActionId = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedCurrentActionId);

                var jsonSerializedDbConnectionStringName = dataMap.GetString("jsonSerializedDbConnectionStringName");
                var dbConnectionStringName = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonSerializedDbConnectionStringName);

                #endregion

                var tellstickCommander = new TellstickUnitDealer(dbConnectionStringName);

                //perform the work we came here for
                this.PerformWork(actionTypeOption: currentTellstickActionType, commander: tellstickCommander, nativeDeviceId: nativeDeviceId);

                //if we reach this point we have succeeded in sending a message to TellstickUnit
                var performedActionsDealer = new PerformedActionsDealer(dbConnectionStringName);

                var actionPerformedTime = DateTime.Now;

                //let's register (to db) what we have done
                performedActionsDealer.Register(currentActionId, actionPerformedTime);

                log.Debug(String.Format("Success with Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));
            }
            catch (Exception ex)
            {
                log.Fatal(String.Format("Failed in executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group), ex);
            }
        }

        private bool PerformWork(ActionTypeOption actionTypeOption, ITellstickUnitDealer commander, int nativeDeviceId)
        {
            var workPerformed = false;

            switch (actionTypeOption)
            {
                case ActionTypeOption.TurnOn:
                    commander.TurnOnDevice(nativeDeviceId);
                    workPerformed = true;
                    break;
                case ActionTypeOption.TurnOff:
                    commander.TurnOffDevice(nativeDeviceId);
                    workPerformed = true;
                    break;
                default:
                    workPerformed = false;
                    throw new ArgumentOutOfRangeException();
            }


            return workPerformed;
        }
    }
}
 