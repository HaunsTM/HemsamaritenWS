//Here is the once-per-application setup information

using Newtonsoft.Json;
using Tellstick.BLL;
using Tellstick.BLL.Interfaces;
using Tellstick.Model.ViewModel;

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

        public bool RefreshBearerToken()
        {
            bool refreshed = false;
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    refreshed = tellstickUnitDealer.RefreshBearerToken();

                    var refreshMessage = "Refreshed bearer token to Tellstick Live";
                    log.Debug(refreshMessage);
                }
            }
            catch (Exception ex)
            {
                var refreshMessage = "Failed in refreshing bearer token to Tellstick Live";
                log.Error(refreshMessage, ex);
            }
            return refreshed;
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

        public string TurnOnTellstickDevice(string name)
        {
            var returnMessage = "";
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOnAndRegisterPerformedAction(name);

                    returnMessage = String.Format("Turned on Tellstick unitId = {0}", name);
                    log.Debug(returnMessage);
                }
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in turning on Tellstick unitId = {0}. Reason: {1}", name, ex.Message);
                log.Error(String.Format("Failed in turning on Tellstick unitId = {0}", name), ex);
            }
            return returnMessage;
        }

        public string TurnOffTellstickDevice(string name)
        {
            var returnMessage = "";
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOffAndRegisterPerformedAction(name);

                    returnMessage = String.Format("Turned off Tellstick unitId = {0}", name);
                    log.Debug(returnMessage);
                }
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in turning off Tellstick unitId = {0}. Reason: {1}", name, ex.Message);
                log.Error(String.Format("Failed in turning off Tellstick unitId = {0}", name), ex);
            }
            return returnMessage;
        }

        #endregion

        public LastPerformedTellstickAction LastPerformedAction(string name)
        {
            var lastPerformedAction = new LastPerformedTellstickAction();
            try
            {
                lock (_syncRoot)
                {
                    var performedActionsDealer = new Tellstick.BLL.PerformedActionsDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    lastPerformedAction = performedActionsDealer.LastPerformedAction(name);
                    
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in turning off Tellstick unitId = {0}", name), ex);
            }
            return lastPerformedAction;

        }

        #endregion
    }
}
