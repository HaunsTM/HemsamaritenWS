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
        private ITellStickZNetCommander NativeCommander { get; set; }
        private IActionsDealer ActionsDealer { get; set; }

        public TellstickUnitDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
            NativeCommander = new TellStickZNetCommander(dbConnectionStringName);
            ActionsDealer = new ActionsDealer(dbConnectionStringName: dbConnectionStringName);
        }
        
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
        public bool ManualTurnOnAndRegisterPerformedActionNative(int nativeDeviceId)
        {
            var turnedOnMessageSent = false;

            try
            {
                this.TurnOnDevice(nativeDeviceId);
                turnedOnMessageSent = true;
                this.RegisterManualPerformedAction_TurnOnNative(nativeDeviceId: nativeDeviceId, time: DateTime.Now);
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
        public bool ManualTurnOffAndRegisterPerformedActionNative(int nativeDeviceId)
        {

            var turnedOffMessageSent = false;

            try
            {
                this.TurnOffDevice(nativeDeviceId);
                turnedOffMessageSent = true;
                this.RegisterManualPerformedAction_TurnOffNative(nativeDeviceId: nativeDeviceId, time: DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return turnedOffMessageSent;
        }

        private Unit UnitBy(string name)
        {
            Unit dbUnit = null;
            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(this.DbConnectionStringName))
                {
                    dbUnit = (from u in db.Units
                              where u.Active == true && u.Name == name
                              select u).First();
                    return dbUnit;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return dbUnit;
        }

        /// <summary>
        /// Turns a device on. (with registration to PerformedAction table)
        /// </summary>
        /// <param name="nativeDeviceId">Id of device to turn on</param>
        /// <returns>If turn on message were sent</returns>
        public bool ManualTurnOnAndRegisterPerformedAction(string name)
        {
            var turnedOnMessageSent = false;
            try
            {
                var currentUnit = UnitBy(name);
                this.TurnOnDevice(nativeDeviceId: currentUnit.NativeDeviceId);
                turnedOnMessageSent = true;
                this.RegisterManualPerformedAction_TurnOnNative(nativeDeviceId: currentUnit.NativeDeviceId, time: DateTime.Now);
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
        public bool ManualTurnOffAndRegisterPerformedAction(string name)
        {

            var turnedOffMessageSent = false;

            try
            {
                var currentUnit = UnitBy(name);
                this.TurnOffDevice(nativeDeviceId: currentUnit.NativeDeviceId);
                turnedOffMessageSent = true;
                this.RegisterManualPerformedAction_TurnOffNative(nativeDeviceId: currentUnit.NativeDeviceId, time: DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return turnedOffMessageSent;
        }

        private bool RegisterManualPerformedAction_TurnOnNative(int nativeDeviceId, DateTime time)
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

        private bool RegisterManualPerformedAction_TurnOffNative(int nativeDeviceId, DateTime time)
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

        /// <summary>
        /// Refreshes the bearer token for communication with the tellstick 
        /// </summary>
        public bool RefreshBearerToken()
        {
            var refreshed = false;
            try
            {
                refreshed =
                    this.NativeCommander.RefreshBearerToken(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't refresh the bearer token for communication with the tellstick.", ex);
            }
            return refreshed;
        }
    }
}