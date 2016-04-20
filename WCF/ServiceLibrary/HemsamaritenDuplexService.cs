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
        
        #endregion

        #region ISurveillanceCam2DBDuplexService
        
        private SurveillanceCam2DB.BLL.JobScheduler SurveillanceCam2DBJobScheduler { get; set; }

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

        public void CreateAndInitializeSurveillanceCam2DB()
        {
            try
            {
                lock (_syncRoot)
                {
                    System.Data.Entity.Database.SetInitializer(new SurveillanceCam2DB.Model.DefaultDataDbInitializer());
                    using (var db = new SurveillanceCam2DB.Model.SurveillanceCam2DBContext(DB_CONNECTION_STRING_NAME__SURVEILLANCE_CAM_2_DB))
                    {
                        db.Database.Initialize(true);

                        //do something random stupid to force seed
                        var stupidValue = db.Images.Count();
                    }
                    log.Debug(String.Format("Initialized surveillance cam DB!"));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in initializing surveillance cam DB!"), ex);
            }
        }

        #endregion

        #region Tellstick

        private Tellstick.BLL.JobScheduler TellstickJobScheduler { get; set; }

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

        public void CreateAndInitializeTellstickDB()
        {
            try
            {
                lock (_syncRoot)
                {
                    System.Data.Entity.Database.SetInitializer(new Tellstick.Model.DefaultDataDbInitializer());
                    using (var db = new Tellstick.Model.TellstickDBContext(DB_CONNECTION_STRING_NAME__TELLSTICK_DB))
                    {
                        db.Database.Initialize(true);

                        //do something random stupid to force seed
                        var stupidValue = db.Models.Count();
                    }
                    log.Debug(String.Format("Initialized TellstickDB!"));
                }
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Failed in initializing TellstickDB!"), ex);
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
