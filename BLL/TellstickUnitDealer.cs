namespace Tellstick.BLL
{
    using Tellstick.BLL.Interfaces;
    using Tellstick.Model;
    using Tellstick.Model.Enums;

    using System;
    using System.ComponentModel;
    using System.Linq;

    using log4net;

    using Tellstick.Model.Interfaces;

    public class TellstickUnitDealer : ITellstickUnitDealer
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public INativeTellstickCommander NativeCommander { get; private set; }
        public string DbConnectionStringName { get; private set; }

        public TellstickUnitDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
            NativeCommander = new NativeTellstickCommander();
        }

        public TellstickUnitDealer(string dbConnectionStringName, INativeTellstickCommander nativeCommander)
        {
            DbConnectionStringName = dbConnectionStringName;
            NativeCommander = nativeCommander;
        }

        /// <summary>
        /// Register a Tellstick device to native Tellstick system AND database
        /// </summary>
        /// <param name="name">Example: Kitchen lamp switch</param>
        /// <param name="locationDesciption">Example: Over the table in the kitchen</param>
        /// <param name="protocol">Example: "arctech"</param>
        /// <param name="modelType">Example: "codeswitch"</param>
        /// <param name="modelManufacturer">Example: "nexa"</param> 
        /// <param name="unit">Example: "1"</param>
        /// <param name="house">Example: "F"</param>
        /// <returns>Registered device id</returns>
        public TellstickUnit AddDevice(string name, string locationDesciption, EnumTellstickProtocol protocol, EnumTellstickModelType modelType, EnumTellstickModelManufacturer modelManufacturer, EnumTellstickParameter_Unit unit, EnumTellstickParameter_House house)
        {
            int addedDeviceNativeId = -1;
            TellstickUnit deviceAddedToDatabase = null;

            try
            {
                 addedDeviceNativeId = AddDeviceToNativeSystem(
                    name,
                    protocol,
                    modelType,
                    modelManufacturer,
                    unit,
                    house);
                deviceAddedToDatabase = AddDeviceToDatabase(
                    addedDeviceNativeId,
                    name,
                    locationDesciption,
                    protocol,
                    modelType,
                    modelManufacturer,
                    unit,
                    house);
            }
            catch (Exception ex)
            {
                log.Error("Could not register Tellstick device!", ex);

                if (addedDeviceNativeId == -1)
                {
                    //we couln't register a device natively

                    //not much to do!
                }
                else if (addedDeviceNativeId != -1 && deviceAddedToDatabase == null)
                {
                    // the device has been registered natively but we couln't register it to the database

                    //try to remove the device from the native register
                    this.NativeCommander.RemoveDevice(addedDeviceNativeId);

                }
                throw ex;
            }
            return deviceAddedToDatabase;
        }

        private int AddDeviceToNativeSystem(string name, EnumTellstickProtocol protocol, EnumTellstickModelType modelType, EnumTellstickModelManufacturer modelManufacturer, EnumTellstickParameter_Unit unit, EnumTellstickParameter_House house)
        {
            var nameSet = name;
            var protocolSet = protocol.GetAttributeOfType<DescriptionAttribute>().Description;
            var modelTypeSet = modelType.GetAttributeOfType<DescriptionAttribute>().Description;
            var modelManufacturerSet = modelManufacturer.GetAttributeOfType<DescriptionAttribute>().Description;

            var unitSet = unit.GetAttributeOfType<DescriptionAttribute>().Description;
            var houseSet = house.GetAttributeOfType<DescriptionAttribute>().Description;

            var addedDeviceId = this.NativeCommander.AddDevice(nameSet, protocolSet, modelTypeSet, modelManufacturerSet, unitSet, houseSet);

            return addedDeviceId;
        }

        private TellstickUnit AddDeviceToDatabase(int nativeDeviceId, string name, string locationDesciption, EnumTellstickProtocol protocol, EnumTellstickModelType modelType, EnumTellstickModelManufacturer modelManufacturer, EnumTellstickParameter_Unit unit, EnumTellstickParameter_House house)
        {
            using (var db = new Model.TellstickDBContext(DbConnectionStringName))
            {
                var protocolToUse = (from prot in db.TellstickProtocols where prot.Active == true && prot.Type == protocol select prot).First();
                var modelToUse = (from model in db.TellstickModels where model.Active == true && model.Type == modelType select model).First();
                var parameterToUse = (from par in db.TellstickParameters where par.Unit == unit && par.House == house select par).First();

                var tellstickUnitAdded =
                db.TellstickUnits.Add(
                    new TellstickUnit
                    {
                        Active = true,
                        Name = name,
                        LocationDesciption = locationDesciption,
                        TellstickProtocol = protocolToUse,
                        TellstickModel = modelToUse,
                        TellstickParameter = parameterToUse,
                        NativeDeviceId = nativeDeviceId
                    });
                db.SaveChanges();

                return tellstickUnitAdded;
            }
        }

        public bool RemoveDevice(int nativeDeviceId)
        {
            var deviceRemoved = false;
            try
            {
                TellstickUnit dbTellstickUnit = null;
                //1. Which TellstickUnit are we talking about? Get TellstickUnit from DB
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbTellstickUnit = (from tellstickUnit in db.TellstickUnits
                    where tellstickUnit.Active == true && tellstickUnit.NativeDeviceId == nativeDeviceId
                    select tellstickUnit).First();
                }
                
                //2. Change tellstickUnit in disconnected mode (out of db scope)
                if (dbTellstickUnit != null)
                {
                    //When "removing" TellstickUnit from database, just change its Active-property to false
                    dbTellstickUnit.Active = false;
                }

                //save modified entity using new Context
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    //3. Mark entity as modified
                    db.Entry(dbTellstickUnit).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    db.SaveChanges();
                }

                //remove the device from native register
                this.NativeCommander.RemoveDevice(nativeDeviceId);

                deviceRemoved = true;
            }
            catch (Exception ex)
            {
                log.Error("Couldn't remove TellstickUnit.", ex);
                throw ex;
            }
            return deviceRemoved;
        }

        public bool RemoveDevice(ITellstickUnit tellstickUnit)
        {
            var deviceRemoved = false;
            var nativeDeviceId = -1;
            try
            {
                TellstickUnit dbTellstickUnit = null;

                //1. Which TellstickUnit are we talking about? Get TellstickUnit from DB
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbTellstickUnit = (from tU in db.TellstickUnits
                                       where tU.Active == true && tU.NativeDeviceId == tellstickUnit.NativeDeviceId
                                       select tU).First();
                }

                //2. Change tellstickUnit in disconnected mode (out of db scope)
                if (dbTellstickUnit != null)
                {
                    //When "removing" TellstickUnit from database, just change its Active-property to false
                    dbTellstickUnit.Active = false;
                    nativeDeviceId = dbTellstickUnit.NativeDeviceId;
                }
                else
                {
                    throw new ArgumentNullException("Coldn't find provided TellstickUnit!");
                }

                //save modified entity using new Context
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    //3. Mark entity as modified
                    db.Entry(dbTellstickUnit).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    db.SaveChanges();
                }

                //remove the device from native register
                this.NativeCommander.RemoveDevice(nativeDeviceId);

                deviceRemoved = true;
            }
            catch (Exception ex)
            {
                log.Error("Couldn't remove TellstickUnit.", ex);
                throw ex;
            }
            return deviceRemoved;
        }

        public bool TurnOnDevice(ITellstickUnit tellstickUnit)
        {
            var deviceTurnedOn = false;
            var nativeDeviceId = -1;
            try
            {
                TellstickUnit dbTellstickUnit = null;

                //Which TellstickUnit are we talking about? Get TellstickUnit from DB
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbTellstickUnit = (from tU in db.TellstickUnits
                                       where tU.Active == true && tU.NativeDeviceId == tellstickUnit.NativeDeviceId
                                       select tU).First();
                }

                if (dbTellstickUnit != null)
                {
                    nativeDeviceId = dbTellstickUnit.NativeDeviceId;

                    //turn on the device
                    this.NativeCommander.TurnOn(nativeDeviceId);

                    deviceTurnedOn = true;
                }
                else
                {
                    throw new ArgumentNullException("Coldn't find provided TellstickUnit!");
                }
            }
            catch (Exception ex)
            {
                log.Error("Couldn't turn on TellstickUnit.", ex);
                throw ex;
            }
            return deviceTurnedOn;
        }

        public bool TurnOnDevice(int nativeDeviceId)
        {
            var deviceTurnedOn = false;
            try
            {
                //turn on the device
                this.NativeCommander.TurnOn(nativeDeviceId);

                deviceTurnedOn = true;
            }
            catch (Exception ex)
            {
                log.Error("Couldn't turn on TellstickUnit.", ex);
                throw ex;
            }
            return deviceTurnedOn;
        }

        public bool TurnOffDevice(ITellstickUnit tellstickUnit)
        {
            var deviceTurnedOff = false;
            var nativeDeviceId = -1;
            try
            {
                TellstickUnit dbTellstickUnit = null;

                //Which TellstickUnit are we talking about? Get TellstickUnit from DB
                using (var db = new Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbTellstickUnit = (from tU in db.TellstickUnits
                                       where tU.Active == true && tU.NativeDeviceId == tellstickUnit.NativeDeviceId
                                       select tU).First();
                }

                if (dbTellstickUnit != null)
                {
                    nativeDeviceId = dbTellstickUnit.NativeDeviceId;

                    //turn on the device
                    this.NativeCommander.TurnOn(nativeDeviceId);

                    deviceTurnedOff = true;
                }
                else
                {
                    throw new ArgumentNullException("Coldn't find provided TellstickUnit!");
                }
            }
            catch (Exception ex)
            {
                log.Error("Couldn't turn on TellstickUnit.", ex);
                throw ex;
            }
            return deviceTurnedOff;
        }

        public bool TurnOffDevice(int nativeDeviceId)
        {
            var deviceTurnedOff = false;
            try
            {
                //turn on the device
                this.NativeCommander.TurnOn(nativeDeviceId);

                deviceTurnedOff = true;
            }
            catch (Exception ex)
            {
                log.Error("Couldn't turn on TellstickUnit.", ex);
                throw ex;
            }
            return deviceTurnedOff;
        }
    }
}