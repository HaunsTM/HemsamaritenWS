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
        
        public string DbConnectionStringName { get; private set; }
        private INativeTellstickCommander NativeCommander { get; set; }
        private IActionsDealer ActionsDealer { get; set; }

        public TellstickUnitDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
            NativeCommander = new NativeTellstickCommander();
            ActionsDealer = new ActionsDealer(dbConnectionStringName: dbConnectionStringName);
        }

        public TellstickUnitDealer(string dbConnectionStringName, INativeTellstickCommander nativeCommander)
        {
            DbConnectionStringName = dbConnectionStringName;
            NativeCommander = nativeCommander;
        }

        #region AddDevice

        /// <summary>
        /// Register a Tellstick device to native Tellstick system AND database
        /// </summary>
        /// <param name="name">Example: Kitchen lamp switch</param>
        /// <param name="locationDesciption">Example: Over the table in the kitchen</param>
        /// <param name="protocolOption">Example: "arctech"</param>
        /// <param name="modelTypeOption">Example: "codeswitch"</param>
        /// <param name="modelManufacturerOption">Example: "nexa"</param> 
        /// <param name="unitOption">Example: "1"</param>
        /// <param name="houseOption">Example: "F"</param>
        /// <returns>Registered device id</returns>
        public Unit AddDevice(string name, string locationDesciption, ProtocolOption protocolOption, ModelTypeOption modelTypeOption, ModelManufacturerOption modelManufacturerOption, Parameter_UnitOption unitOption, Parameter_HouseOption houseOption)
        {
            int addedDeviceNativeId = -1;
            Unit deviceAddedToDatabase = null;

            try
            {
                 addedDeviceNativeId = AddDeviceToNativeSystem(
                    name,
                    protocolOption,
                    modelTypeOption,
                    modelManufacturerOption,
                    unitOption,
                    houseOption);
                deviceAddedToDatabase = AddDeviceToDatabase(
                    addedDeviceNativeId,
                    name,
                    locationDesciption,
                    protocolOption,
                    modelTypeOption,
                    modelManufacturerOption,
                    unitOption,
                    houseOption);
            }
            catch (Exception ex)
            {

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
                throw new Exception("Could not register Tellstick device!", ex);
            }
            return deviceAddedToDatabase;
        }

        private int AddDeviceToNativeSystem(string name, ProtocolOption protocolOption, ModelTypeOption modelTypeOption, ModelManufacturerOption modelManufacturerOption, Parameter_UnitOption unitOption, Parameter_HouseOption houseOption)
        {
            var nameSet = name;
            var protocolSet = protocolOption.GetAttributeOfType<DescriptionAttribute>().Description;
            var modelTypeSet = modelTypeOption.GetAttributeOfType<DescriptionAttribute>().Description;
            var modelManufacturerSet = modelManufacturerOption.GetAttributeOfType<DescriptionAttribute>().Description;

            var unitSet = unitOption.GetAttributeOfType<DescriptionAttribute>().Description;
            var houseSet = houseOption.GetAttributeOfType<DescriptionAttribute>().Description;

            var addedDeviceId = this.NativeCommander.AddDevice(nameSet, protocolSet, modelTypeSet, modelManufacturerSet, unitSet, houseSet);

            return addedDeviceId;
        }

        private Unit AddDeviceToDatabase(int nativeDeviceId, string name, string locationDesciption, ProtocolOption protocolOption, ModelTypeOption modelTypeOption, ModelManufacturerOption modelManufacturerOption, Parameter_UnitOption unitOption, Parameter_HouseOption houseOption)
        {
            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                var protocolToUse = (from prot in db.Protocols where prot.Active == true && prot.Type == protocolOption select prot).First();
                var modelToUse = (from model in db.Models where model.Active == true && model.TypeOption == modelTypeOption select model).First();
                var parameterToUse = (from par in db.Parameters where par.UnitOption == unitOption && par.HouseOption == houseOption select par).First();

                var tellstickUnitAdded =
                db.Units.Add(
                    new Unit
                    {
                        Active = true,
                        Name = name,
                        LocationDesciption = locationDesciption,
                        Protocol = protocolToUse,
                        Model = modelToUse,
                        Parameter = parameterToUse,
                        NativeDeviceId = nativeDeviceId
                    });
                db.SaveChanges();

                return tellstickUnitAdded;
            }
        }

        #endregion

        #region RemoveDevice

        public bool RemoveDevice(int nativeDeviceId)
        {
            var deviceRemoved = false;
            try
            {
                Unit dbUnit = null;
                //1. Which Unit are we talking about? Get Unit from DB
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbUnit = (from tellstickUnit in db.Units
                    where tellstickUnit.Active == true && tellstickUnit.NativeDeviceId == nativeDeviceId
                    select tellstickUnit).First();
                }
                
                //2. Change unit in disconnected mode (out of db scope)
                if (dbUnit != null)
                {
                    //When "removing" Unit from database, just change its Active-property to false
                    dbUnit.Active = false;
                }

                //save modified entity using new Context
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    //3. Mark entity as modified
                    db.Entry(dbUnit).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    db.SaveChanges();
                }

                //remove the device from native register
                this.NativeCommander.RemoveDevice(nativeDeviceId);

                deviceRemoved = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't remove TellstickUnit.", ex);
            }
            return deviceRemoved;
        }

        public bool RemoveDevice(IUnit unit)
        {
            var deviceRemoved = false;
            var nativeDeviceId = -1;
            try
            {
                Unit dbUnit = null;

                //1. Which Unit are we talking about? Get kUnit from DB
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbUnit = (from tU in db.Units
                                       where tU.Active == true && tU.NativeDeviceId == unit.NativeDeviceId
                                       select tU).First();
                }

                //2. Change Unit in disconnected mode (out of db scope)
                if (dbUnit != null)
                {
                    //When "removing" Unit from database, just change its Active-property to false
                    dbUnit.Active = false;
                    nativeDeviceId = dbUnit.NativeDeviceId;
                }
                else
                {
                    throw new ArgumentNullException("Coldn't find provided TellstickUnit!");
                }

                //save modified entity using new Context
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    //3. Mark entity as modified
                    db.Entry(dbUnit).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    db.SaveChanges();
                }

                //remove the device from native register
                this.NativeCommander.RemoveDevice(nativeDeviceId);

                deviceRemoved = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't remove TellstickUnit.", ex);
            }
            return deviceRemoved;
        }

        #endregion

        #region TurnOnDevice (without registration to PerformedAction table)

        public bool TurnOnDevice(IUnit unit)
        {
            var deviceTurnedOn = false;
            var nativeDeviceId = -1;
            try
            {
                Unit dbUnit = null;

                //Which Unit are we talking about? Get Unit from DB
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbUnit = (from tU in db.Units
                                       where tU.Active == true && tU.NativeDeviceId == unit.NativeDeviceId
                                       select tU).First();
                }

                if (dbUnit != null)
                {
                    nativeDeviceId = dbUnit.NativeDeviceId;

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
                throw ex;
            }
            return deviceTurnedOn;
        }

        /// <summary>
        /// Turns a device on. (without registration to PerformedAction table)
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
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
                throw ex;
            }
            return deviceTurnedOn;
        }

        #endregion

        #region TurnOffDevice (without registration to PerformedAction table)

        public bool TurnOffDevice(IUnit unit)
        {
            var deviceTurnedOff = false;
            var nativeDeviceId = -1;
            try
            {
                Unit dbUnit = null;

                //Which TellstickUnit are we talking about? Get TellstickUnit from DB
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbUnit = (from tU in db.Units
                                       where tU.Active == true && tU.NativeDeviceId == unit.NativeDeviceId
                                       select tU).First();
                }

                if (dbUnit != null)
                {
                    nativeDeviceId = dbUnit.NativeDeviceId;

                    //turn off the device
                    this.NativeCommander.TurnOff(nativeDeviceId);

                    deviceTurnedOff = true;
                }
                else
                {
                    throw new ArgumentNullException("Coldn't find provided TellstickUnit!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return deviceTurnedOff;
        }

        /// <summary>
        /// Turns a device on. (without registration to PerformedAction table)
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn off message were sent</returns>
        public bool TurnOffDevice(int nativeDeviceId)
        {
            var deviceTurnedOff = false;
            try
            {
                //turn on the device
                this.NativeCommander.TurnOff(nativeDeviceId);

                deviceTurnedOff = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't turn off TellstickUnit.", ex);
            }
            return deviceTurnedOff;
        }

        #endregion

        #region Methods that should be used when dealing with manual turn on/off

        /// <summary>
        /// Turns a device on. (with registration to PerformedAction table)
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        public bool ManualTurnOnAndRegisterPerformedAction(int nativeDeviceId)
        {
            var turnedOnMessageSent = false;

            try
            {
                this.TurnOnDevice(nativeDeviceId);
                turnedOnMessageSent = true;
                this.RegisterManualPerformedAction_TurnOn(nativeDeviceId: nativeDeviceId, time: DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return turnedOnMessageSent;
        }

        /// <summary>
        /// Turns a device off. (with registration to PerformedAction table)
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn off</param>
        /// <returns>If turn off message were sent</returns>
        public bool ManualTurnOffAndRegisterPerformedAction(int nativeDeviceId)
        {

            var turnedOffMessageSent = false;

            try
            {
                this.TurnOffDevice(nativeDeviceId);
                turnedOffMessageSent = true;
                this.RegisterManualPerformedAction_TurnOff(nativeDeviceId: nativeDeviceId, time: DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return turnedOffMessageSent;
        }

        private bool RegisterManualPerformedAction_TurnOn(int nativeDeviceId, DateTime time)
        {
            var registered = false;
            try
            {
                Tellstick.Model.Action usedAction = null;

                //do we have an Action in db for this event already?
                var possibleRegisteredAction = ActionsDealer.ActionExists(nativeDeviceId: nativeDeviceId, actionTypeOption: ActionTypeOption.TurnOn, scheduler: null);

                usedAction = possibleRegisteredAction;

                if (possibleRegisteredAction == null)
                {
                    //no we haven't, register an new Action
                    usedAction = ActionsDealer.RegisterNewManualAction(nativeDeviceId: nativeDeviceId, actionTypeOption: ActionTypeOption.TurnOn);
                }

                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    var performedAction = new PerformedAction { Active = true, Action_Id = usedAction.Id, Time = time };
                    db.PerformedActions.Add(performedAction);
                    db.SaveChanges();
                    registered = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Couldn't register manual {0} to db for Tellstick.NativeDeviceId={1} at time={2}.", ActionTypeOption.TurnOn.GetAttributeOfType<DescriptionAttribute>().Description, nativeDeviceId, time), ex);
            }

            return registered;
        }

        private bool RegisterManualPerformedAction_TurnOff(int nativeDeviceId, DateTime time)
        {
            var registered = false;
            try
            {
                Tellstick.Model.Action usedAction = null;

                //do we have an Action in db for this event already?
                var possibleRegisteredAction = ActionsDealer.ActionExists(nativeDeviceId: nativeDeviceId, actionTypeOption: ActionTypeOption.TurnOff, scheduler: null);

                usedAction = possibleRegisteredAction;

                if (possibleRegisteredAction == null)
                {
                    //no we haven't, register an new Action
                    usedAction = ActionsDealer.RegisterNewManualAction(nativeDeviceId: nativeDeviceId, actionTypeOption: ActionTypeOption.TurnOff);
                }

                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    var performedAction = new PerformedAction { Active = true, Action_Id = usedAction.Id, Time = time };
                    db.PerformedActions.Add(performedAction);
                    db.SaveChanges();
                    registered = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Couldn't register manual {0} to db for Tellstick.NativeDeviceId={1} at time={2}.", ActionTypeOption.TurnOff.GetAttributeOfType<DescriptionAttribute>().Description, nativeDeviceId, time), ex);
            }

            return registered;
        }

        #endregion
    }
}