//Here is the once-per-application setup information
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
            this.SurveillanceCam2DBJobScheduler = null;
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

        #region ISurveillanceCam2DBDuplexService
        
        private SurveillanceCam2DB.BLL.JobScheduler SurveillanceCam2DBJobScheduler { get; set; }

        public void DumpCurrentlyExecutingSurveillanceCam2DBJobsNamesToLog()
        {
            try
            {
                lock (_syncRoot)
                {
                    if (this.SurveillanceCam2DBJobScheduler != null)
                    {
                        var currentlyExecutingSurveillanceCam2DBJobsNamesList = this.SurveillanceCam2DBJobScheduler.CurrentlyExecutingJobsNames;
                        if (currentlyExecutingSurveillanceCam2DBJobsNamesList.Count > 0)
                        {
                            var currentlyExecutingSurveillanceCam2DBJobsNames = string.Join("; ", currentlyExecutingSurveillanceCam2DBJobsNamesList);
                            log.Debug(String.Format("Currently executing SurveillanceCam2DB jobs: {0}", currentlyExecutingSurveillanceCam2DBJobsNames));
                        }
                        else
                        {
                            log.Debug(String.Format("No SurveillanceCam2DB jobs executing currently!"));
                        }
                    }
                    else
                    {
                        log.Debug(String.Format("Class SurveillanceCam2DBJobScheduler is not instantiated! Run StartSurveillanceCam2DBScheduler() first!"));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in getting running Tellstick jobs."), ex);
            }
        }

        public void StartSurveillanceCam2DBScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    this.SurveillanceCam2DBJobScheduler = new SurveillanceCam2DB.BLL.JobScheduler(DB_CONNECTION_STRING_NAME__SURVEILLANCE_CAM_2_DB);
                    this.SurveillanceCam2DBJobScheduler.Start();
                    log.Debug(String.Format("Started SurveillanceCam2DBScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in starting SurveillanceCam2DBScheduler."), ex);
            }
        }

        public void StopSurveillanceCam2DBScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    this.SurveillanceCam2DBJobScheduler.Stop();
                    this.SurveillanceCam2DBJobScheduler = null;
                    log.Debug(String.Format("Stopped SurveillanceCam2DBScheduler."));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in stopping SurveillanceCam2DBScheduler."), ex);
            }
        }

        /// <summary>
        /// Creates and initializes a database
        /// </summary>
        public void CreateAndInitializeSurveillanceCam2DB()
        {
            try
            {
                lock (_syncRoot)
                {
                    var databaseDealer = new SurveillanceCam2DB.BLL.DatabaseDealer(DB_CONNECTION_STRING_NAME__SURVEILLANCE_CAM_2_DB);

                    var databaseCreated = databaseDealer.CreateAndInitializeSurveillanceCam2DBDB();
                    if (databaseCreated)
                    {
                        log.Debug(String.Format("Created and initialized SurveillanceCam2DB!"));
                    }
                    else
                    {
                        throw new Exception(String.Format("Failed in creating and initializing SurveillanceCam2DB."));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in creating and initializing SurveillanceCam2DB."), ex);
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
        
        public Tellstick.Model.Unit RegisterTellstickDevice(
            string name,
            string locationDesciption,
            Tellstick.Model.Enums.ProtocolOption protocolOption,
            Tellstick.Model.Enums.ModelTypeOption modelTypeOption,
            Tellstick.Model.Enums.ModelManufacturerOption modelManufacturerOption,
            Tellstick.Model.Enums.Parameter_UnitOption unitOption,
            Tellstick.Model.Enums.Parameter_HouseOption houseOption)
        {
            var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
            var addedUnit = tellstickUnitDealer.AddDevice(name, locationDesciption, protocolOption, modelTypeOption, modelManufacturerOption, unitOption, houseOption);
            return addedUnit;
        }

        public void RemoveTellstickDevice(int nativeDeviceId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.RemoveDevice(nativeDeviceId);

                    log.Debug(String.Format("Removed Tellstick nativeDeviceId = {0}", nativeDeviceId));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in removing Tellstick nativeDeviceId = {0}", nativeDeviceId), ex);
            }
        }

        public void TurnOnTellstickDevice(int nativeDeviceId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOnAndRegisterPerformedAction(nativeDeviceId);

                    log.Debug(String.Format("Turned on Tellstick nativeDeviceId = {0}", nativeDeviceId));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in turning off Tellstick nativeDeviceId = {0}", nativeDeviceId), ex);
            }
        }

        public void TurnOffTellstickDevice(int nativeDeviceId)
        {
            try
            {
                lock (_syncRoot)
                {
                    var tellstickUnitDealer = new Tellstick.BLL.TellstickUnitDealer(DB_CONNECTION_STRING_NAME__TELLSTICK_DB);
                    tellstickUnitDealer.ManualTurnOffAndRegisterPerformedAction(nativeDeviceId);

                    log.Debug(String.Format("Turned off Tellstick nativeDeviceId = {0}", nativeDeviceId));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in turning off Tellstick nativeDeviceId = {0}", nativeDeviceId), ex);
            }
        }

        #endregion
    }
}
