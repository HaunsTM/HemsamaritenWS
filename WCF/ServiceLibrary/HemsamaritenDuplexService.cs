//Here is the once-per-application setup information
using Core.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Core.Model;
using WCF.ServiceLibrary.Interfaces;
using System.Net.Http;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace WCF.ServiceLibrary
{

    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class  HemsamaritenDuplexService : ServiceBase, IHemsamaritenDuplexService
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<IHemsamaritenDuplexCallback> _callbackChannels = new List<IHemsamaritenDuplexCallback>();
        private static readonly object _syncRoot = new object();

        #region DB connection strings
        
        private const string DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE = "name=HemsamaritenWindowsServiceDBConnection";
        private const string DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE_DEBUG_LOG = "name=HemsamaritenWindowsServiceDebugLogDBConnection";

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
                    var databaseDealer = new WCF.BLL.DatabaseDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE_DEBUG_LOG);

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

        #region Scheduler

        
        public void StartAllSchedulers()
        {
            this.StartTellstickScheduler();
            this.StartMediaScheduler();
        }
        
        public void StopAllSchedulers()
        {
            this.StopTellstickScheduler();
            this.StopMediaScheduler();
        }

        #endregion

        #endregion

        #region Tellstick

        private Core.BLL.TellstickJobScheduler TellstickJobScheduler { get; set; }

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
                    var tellstickUnitDealer = new Core.BLL.TellstickUnitDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
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
                    this.TellstickJobScheduler = new Core.BLL.TellstickJobScheduler(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
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
                var databaseDealer = new Core.BLL.DatabaseDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);

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
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in creating and initializing TellstickDB."), ex);
            }
        }

        public string TurnOnTellstickDevice(string name)
        {
            var returnMessage = "";
            try
            {
                var tellstickUnitDealer = new Core.BLL.TellstickUnitDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                tellstickUnitDealer.ManualTurnOnAndRegisterPerformedAction(name);

                returnMessage = String.Format("Turned on Tellstick unitId = {0}", name);
                log.Debug(returnMessage);
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
                var tellstickUnitDealer = new Core.BLL.TellstickUnitDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                tellstickUnitDealer.ManualTurnOffAndRegisterPerformedAction(name);

                returnMessage = String.Format("Turned off Tellstick unitId = {0}", name);
                log.Debug(returnMessage);
                
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in turning off Tellstick unitId = {0}. Reason: {1}", name, ex.Message);
                log.Error(String.Format("Failed in turning off Tellstick unitId = {0}", name), ex);
            }
            return returnMessage;
        }
        
        #endregion

        #region Media


        #region Scheduler

        public void StartMediaScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    var mediaJobScheduler = new Core.BLL.MediaJobScheduler(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                    mediaJobScheduler.Start();

                    log.Debug(String.Format("Started MediaScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in starting MediaScheduler."), ex);
            }
        }

        public void StopMediaScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    var mediaJobScheduler = new Core.BLL.MediaJobScheduler(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                    mediaJobScheduler.Stop();

                    log.Debug(String.Format("Stopped MediaScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in stopping MediaScheduler."), ex);
            }
        }

        #endregion


        public string SetMediaVolume(int value)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.SetVolume(value);

                returnMessage = String.Format("Set volume to = {0}", value.ToString());
                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in setting volume to = {0}. Reason: {1}", value.ToString(), ex.Message);
            }
            return returnMessage;
        }

        public string PlayMedia(string url)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Play(url);

                returnMessage = String.Format("Playing {0}.", url);
                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in playing {0}. Reason {1}", url, ex.Message);
            }
            return returnMessage;
        }

        public string PlayMediaAndSetVolume(string url, int mediaOutputVolume)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Play(url, mediaOutputVolume);

                returnMessage = String.Format("Playing {0} with volume {1}.", url, mediaOutputVolume.ToString());
                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in playing {0} with volume {1}. Reason {2}", url, mediaOutputVolume.ToString(), ex.Message);
            }
            return returnMessage;
        }

        public string StopMediaPlay()
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Stop();

                returnMessage = String.Format("Stop");
                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                returnMessage = String.Format("Failed in stopping. Reason: {0}", ex.Message);
            }
            return returnMessage;
        }

        public List<IRegisteredMediaSource> MediaSourcesList()
        {
            var presetMediaSources = new List<IRegisteredMediaSource>();
            try
            {
                var mediaSourceDealer = new Core.BLL.MediaSourceDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                presetMediaSources = mediaSourceDealer.PredefinedMediaSourcesList();
            }
            catch (Exception ex)
            {
                log.Error($"Failed in getting LastPerformedActionsForAllDevices", ex);
            }
            return presetMediaSources;

        }

        #endregion
    }
}
