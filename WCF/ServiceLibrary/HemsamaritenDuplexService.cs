﻿//Here is the once-per-application setup information

using Newtonsoft.Json;
using Tellstick.BLL;
using Tellstick.BLL.Interfaces;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WCF.ServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;

    using WCF.ServiceLibrary.Interfaces;

    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class HemsamaritenDuplexService : ServiceBase, WCF.ServiceLibrary.Interfaces.IHemsamaritenDuplexService
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<IHemsamaritenDuplexCallback> _callbackChannels = new List<IHemsamaritenDuplexCallback>();
        private static readonly object _syncRoot = new object();

        #region DB connection strings

        private const string DB_CONNECTION_STRING_NAME__SURVEILLANCE_CAM_2_DB = "name=SurveillanceCamerasDBConnection";
        private const string DB_CONNECTION_STRING_NAME__TELLSTICK_DB = "name=TellstickDBConnection";
        private const string DB_CONNECTION_STRING_NAME__HEMSAMARITEN_DB = "name=HemsamaritenDBConnection";

        #endregion

        public HemsamaritenDuplexService()
        {
            log.Debug("HemsamaritenDuplexService started!");
            this.TellstickJobScheduler = null;
        }

        #region IHemsamaritenDuplexService

        /// <summary>
        /// Creates and initializes a database used for f ex log4net
        /// </summary>
        public void CreateAndInitializeHemsamaritenDB()
        {
            try
            {
                lock (_syncRoot)
                {
                    var databaseDealer = new WCF.BLL.DatabaseDealer(DB_CONNECTION_STRING_NAME__HEMSAMARITEN_DB);

                    var databaseCreated = databaseDealer.CreateAndInitializeHemsamaritenDB();
                    if (databaseCreated)
                    {
                        log.Debug(String.Format("Created and initialized HemsamaritenDB!"));
                    }
                    else
                    {
                        throw new Exception(String.Format("Failed in creating and initializing HemsamaritenDB."));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in creating and initializing HemsamaritenDB."), ex);
            }
        }

        #endregion
        
        #region Tellstick

        private Tellstick.BLL.JobScheduler TellstickJobScheduler { get; set; }

        public void DumpCurrentlyExecutingTellstickJobsNamesToLog()
        {
            try
            {
                lock (_syncRoot)
                {
                    if (this.TellstickJobScheduler != null)
                    {
                        var currentlyExecutingTellstickJobsNamesList = TellstickJobScheduler.CurrentlyExecutingJobsNames;
                        if (currentlyExecutingTellstickJobsNamesList.Count > 0)
                        {
                            var currentlyExecutingTellstickJobsNames = string.Join("; ", currentlyExecutingTellstickJobsNamesList);
                            log.Debug(String.Format("Currently executing Tellstick jobs: {0}", currentlyExecutingTellstickJobsNames));
                        }
                        else
                        {
                            log.Debug(String.Format("No Tellstick jobs executing currently!"));
                        }
                    }
                    else
                    {
                        log.Debug(String.Format("Class TellstickJobScheduler is not instantiated! Run StartTellstickScheduler() first!"));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in getting running Tellstick jobs."), ex);
            }
        }
        
        #region Scheduler

        public void StartTellstickScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    this.TellstickJobScheduler = new Tellstick.BLL.JobScheduler(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    this.TellstickJobScheduler.Start();

                    log.Debug(String.Format("Started TellstickScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in starting TellstickScheduler."), ex);
            }
        }

        public void StopTellstickScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    this.TellstickJobScheduler.Stop();
                    this.TellstickJobScheduler = null;

                    log.Debug(String.Format("Stopped TellstickScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in stopping TellstickScheduler."), ex);
            }
        }

        #endregion

        #region Actions

        public string GetAllActions()
        {
            try
            {
                lock (_syncRoot)
                {
                    var actionsDealer = new Tellstick.BLL.ActionsDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    var actions = actionsDealer.GetAllActions();

                    string jsonActions = JsonConvert.SerializeObject(actions,
                        new JsonSerializerSettings { });

                    return jsonActions;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in returning Actions-list for all devices"), ex);
                return null;
            }
        }

        public string GetActionsBy(string unitId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var actionsDealer = new Tellstick.BLL.ActionsDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    
                    var actions = actionsDealer.GetActionsBy(int.Parse(unitId));

                    string jsonActions = JsonConvert.SerializeObject(actions,
                        new JsonSerializerSettings { });
                    return jsonActions;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in returning Actions-list for device unitId={0}", unitId.ToString()), ex);
                return null;
            }
        }

        public string SetActionFor(string unitId, string actionTypeOption, string[] cronExpressions)
        {
            try
            {
                lock (_syncRoot)
                {
                    var actionsDealer = new Tellstick.BLL.ActionsDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    var searchParameters = new ActionSearchParameters { unitId = unitId, actionTypeOption = actionTypeOption, cronExpressions = cronExpressions };
                    var actions = actionsDealer.ActivateActionsFor(searchParameters);

                    string jsonActions = JsonConvert.SerializeObject(actions,
                        new JsonSerializerSettings { });
                    return jsonActions;
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in returning Actions-list for all devices"), ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Creates and initializes a database
        /// </summary>
        public void CreateAndInitializeTellstickDB()
        {
            try
            {
                lock (_syncRoot)
                {
                    var databaseDealer = new Tellstick.BLL.DatabaseDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);

                    var databaseCreated = databaseDealer.CreateAndInitializeTellstickDB();
                    if (databaseCreated)
                    {
                        log.Debug(String.Format("Created and initialized TellstickDB!"));
                    }
                    else
                    {
                        throw new Exception(String.Format("Failed in creating and initializing TellstickDB."));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in creating and initializing TellstickDB."), ex);
            }
        }
        
        #region Turn on/off device

        public void TurnOnTellstickDeviceNative(int nativeDeviceId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOnAndRegisterPerformedActionNative(nativeDeviceId);

                    log.Debug(String.Format("Turned on Tellstick nativeDeviceId = {0}", nativeDeviceId));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in turning off Tellstick nativeDeviceId = {0}", nativeDeviceId), ex);
            }
        }

        public void TurnOffTellstickDeviceNative(int nativeDeviceId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOffAndRegisterPerformedActionNative(nativeDeviceId);

                    log.Debug(String.Format("Turned off Tellstick nativeDeviceId = {0}", nativeDeviceId));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in turning off Tellstick nativeDeviceId = {0}", nativeDeviceId), ex);
            }
        }

        public string TurnOnTellstickDevice(int unitId)
        {
            var returnMessage = "";
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOnAndRegisterPerformedAction(unitId);

                    returnMessage = String.Format("Turned on Tellstick unitId = {0}", unitId);
                    log.Debug(returnMessage);
                }
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in turning on Tellstick unitId = {0}. Reason: {1}", unitId, ex.Message);
                log.Error(String.Format("Failed in turning on Tellstick unitId = {0}", unitId), ex);
            }
            return returnMessage;
        }

        public string TurnOffTellstickDevice(int unitId)
        {
            var returnMessage = "";
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOffAndRegisterPerformedAction(unitId);

                    returnMessage = String.Format("Turned off Tellstick unitId = {0}", unitId);
                    log.Debug(returnMessage);
                }
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in turning off Tellstick unitId = {0}. Reason: {1}", unitId, ex.Message);
                log.Error(String.Format("Failed in turning off Tellstick unitId = {0}", unitId), ex);
            }
            return returnMessage;
        }

        #endregion

    #endregion
    }
}
