namespace Tellstick.BLL
{
    using System;

    using Quartz;

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
            var tellstickCommander = new NativeTellstickCommander();

            log.Debug(String.Format("Executing Job [{0}; {1}] with Trigger [{2}; {3}]", context.JobDetail.Key.Name, context.JobDetail.Key.Group, context.Trigger.Key.Name, context.Trigger.Key.Group));

            try
            {
                #region Job Data

                var jsonSerializedCurrentTellstickActionType = dataMap.GetString("jsonSerializedTellstickActionType");
                var currentTellstickActionType = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionType>(jsonSerializedCurrentTellstickActionType);

                var jsonSerializedCurrentNativeDeviceId = dataMap.GetString("jsonSerializedCurrentNativeDeviceId");
                var nativeDeviceId = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedCurrentNativeDeviceId);

                var jsonSerializedCurrentDimValue = dataMap.GetString("jsonSerializedCurrentDimValue");
                var dimValue = Newtonsoft.Json.JsonConvert.DeserializeObject<char>(jsonSerializedCurrentDimValue);

                var jsonSerializedCurrentActionId = dataMap.GetString("jsonSerializedCurrentActionId");
                var currentActionId = Newtonsoft.Json.JsonConvert.DeserializeObject<int>(jsonSerializedCurrentActionId);

                var jsonSerializedDbConnectionStringName = dataMap.GetString("jsonSerializedDbConnectionStringName");
                var dbConnectionStringName = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonSerializedDbConnectionStringName);

                #endregion

                //perform the work we came here for
                this.PerformWork(actionType: currentTellstickActionType, commander: tellstickCommander, nativeDeviceId: nativeDeviceId, dimValue: dimValue);

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

        private bool PerformWork(ActionType actionType, NativeTellstickCommander commander, int nativeDeviceId, char dimValue)
        {
            var workPerformed = false;

            switch (actionType)
            {
                case ActionType.TurnOn:
                    commander.TurnOn(nativeDeviceId);
                    workPerformed = true;
                    break;
                case ActionType.TurnOff:
                    commander.TurnOff(nativeDeviceId);
                    workPerformed = true;
                    break;
                case ActionType.Dim:
                    commander.Dim(nativeDeviceId, dimValue);
                    workPerformed = false;
                    break;
                default:
                    workPerformed = false;
                    throw new ArgumentOutOfRangeException();
            }


            return workPerformed;
        }
    }
}
 