//Here is the once-per-application setup information
using System;
using WCF.ServiceLibrary.Interfaces;

namespace WCF.ServiceLibrary
{
    public partial class HemsamaritenService
    {
        private Core.BLL.TellstickJobScheduler TellstickJobScheduler { get; set; }

        void ITellstickDuplexService.DumpCurrentlyExecutingTellstickJobsNamesToLog()
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

        bool ITellstickDuplexService.RefreshBearerToken()
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

        void ITellstickDuplexService.StartTellstickScheduler()
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

        void ITellstickDuplexService.StopTellstickScheduler()
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

        /// <summary>
        /// Creates and initializes a database
        /// </summary>
        void ITellstickDuplexService.CreateAndInitializeTellstickDB()
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

        string ITellstickDuplexService.TurnOnTellstickDevice(string name)
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

        string ITellstickDuplexService.TurnOffTellstickDevice(string name)
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

    }
}
