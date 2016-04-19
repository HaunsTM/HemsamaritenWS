namespace Tellstick.BLL
{
    using System;
    using System.Linq;

    using log4net;

    using Tellstick.BLL.Interfaces;

    public class ActionsDealer : IActionsDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public ActionsDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Searches for an Action given certain parameters
        /// </summary>
        /// <param name="nativeDeviceId">Id of the current Tellstick device</param>
        /// <param name="actionTypeOption"></param>
        /// <param name="scheduler"></param>
        /// <returns>An Action if it is found, NULL if it is not found</returns>
        public Tellstick.Model.Action ActionExists(int nativeDeviceId, Tellstick.Model.Enums.ActionTypeOption actionTypeOption, Tellstick.Model.Interfaces.IScheduler scheduler)
        {
            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                Model.Action actionToSearchFor = null;
                //which ActionType are we dealing with?
                var currentActionType = (from actionType in db.ActionTypes
                                         where actionType.ActionTypeOption == actionTypeOption
                                         select actionType).FirstOrDefault();

                //which tellstick Unit are we dealing with?
                var currentUnit = (from unit in db.Units
                                   where unit.NativeDeviceId == nativeDeviceId
                                   select unit).FirstOrDefault();

                switch (scheduler == null)
                {
                    case true:
                        actionToSearchFor = (from existingAction in db.Actions
                                             where
                                                 existingAction.Active == true
                                                 && existingAction.Unit.Id == currentUnit.Id
                                                 && existingAction.ActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;
                    case false:
                        actionToSearchFor = (from existingAction in db.Actions
                                             where
                                                 existingAction.Active == true
                                                 && existingAction.Scheduler.Id == scheduler.Id
                                                 && existingAction.Unit.Id == currentUnit.Id
                                                 && existingAction.ActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;

                }
                
                return actionToSearchFor;
            }
        }

        public Tellstick.Model.Action RegisterNewManualAction(int nativeDeviceId, Tellstick.Model.Enums.ActionTypeOption actionTypeOption)
        {
            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    //which ActionType are we dealing with?
                    var currentActionType = (from actionType in db.ActionTypes
                                             where
                                                 actionType.ActionTypeOption == actionTypeOption
                                             select actionType).First();

                    //which tellstick Unit are we dealing with?
                    var currentUnit = (from unit in db.Units where unit.NativeDeviceId == nativeDeviceId select unit).First();

                    var newManualAction = new Tellstick.Model.Action
                                                {
                                                    Active = true,
                                                    ActionType = currentActionType,
                                                    Unit = currentUnit
                                                };

                    db.Actions.Add(newManualAction);
                    db.SaveChanges();

                    // Return the new action from the existing entity, which was updated when the record was saved to the database
                    return newManualAction;
                    
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not save a new (manual) Action to database!", ex);
                throw ex;
            }
        }

    }
}
