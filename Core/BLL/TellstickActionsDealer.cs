using System;
using System.Collections.Generic;
using System.Linq;
using Core.BLL.Interfaces;
using Core.Model;
using log4net;

namespace Core.BLL
{

    public class ActionSearchParameters : IActionSearchParameters
    {
        public string unitId { get; set; }
        public string actionTypeOption { get; set; }
        public string[] cronExpressions { get; set; }
    }

    public class TellstickActionsDealer : IActionsDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public TellstickActionsDealer(string dbConnectionStringName)
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
        public Core.Model.TellstickAction ActionExists(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Core.Model.Interfaces.IScheduler scheduler)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                Model.TellstickAction actionToSearchFor = null;
                //which ActionType are we dealing with?
                var currentActionType = (from actionType in db.TellstickActionTypes
                                         where actionType.ActionTypeOption == actionTypeOption
                                         select actionType).FirstOrDefault();

                //which tellstick Unit are we dealing with?
                var currentUnit = (from unit in db.TellstickUnits
                                   where unit.NativeDeviceId == nativeDeviceId
                                   select unit).FirstOrDefault();

                switch (scheduler == null)
                {
                    case true:
                        actionToSearchFor = (from existingAction in db.TellstickActions
                                             where
                                                 existingAction.Active == true
                                                 && existingAction.TellstickUnit.Id == currentUnit.Id
                                                 && existingAction.TellstickActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;
                    case false:
                        actionToSearchFor = (from existingAction in db.TellstickActions
                                             where
                                                 existingAction.Active == true
                                                 && existingAction.Scheduler.Id == scheduler.Id
                                                 && existingAction.TellstickUnit.Id == currentUnit.Id
                                                 && existingAction.TellstickActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;

                }
                
                return actionToSearchFor;
            }
        }

        public Core.Model.TellstickAction RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption)
        {
            try
            {
                using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
                {
                    //which ActionType are we dealing with?
                    var currentActionType = (from actionType in db.TellstickActionTypes
                                             where actionType.ActionTypeOption == actionTypeOption
                                             select actionType).First();

                    //which tellstick Unit are we dealing with?
                    var currentUnit = (from unit in db.TellstickUnits where unit.NativeDeviceId == nativeDeviceId select unit).First();

                    var newManualAction = new Core.Model.TellstickAction
                                                {
                                                    Active = true,
                                                    TellstickActionType = currentActionType,
                                                    TellstickUnit = currentUnit
                                                };

                    db.TellstickActions.Add(newManualAction);
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

        public List<Core.Model.TellstickAction> GetAllActions()
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var actions = (from a in db.TellstickActions
                               orderby a.TellstickUnit_Id
                               select a).ToList();
                return actions;
            }
        }

        public IQueryable<Core.Model.TellstickAction> GetActionsBy(int unitId)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var actions = from a in db.TellstickActions
                               where a.TellstickUnit_Id == unitId
                               select a;

                return actions;
            }
        }

        private IQueryable<Core.Model.TellstickAction> GetActionsBy(int unitId, bool activeStatus)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var actions = from a in db.TellstickActions
                    where a.TellstickUnit_Id == unitId && a.Active == activeStatus
                    select a;
                return actions;
            }

        }






        public List<Core.Model.TellstickAction> ActivateActionsFor(IActionSearchParameters searchParameters)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext( DbConnectionStringName ) )
            {
                try
                {
                    var sD = new SchedulerDealer(DbConnectionStringName);
                    var aTD = new ActionTypesDealer(DbConnectionStringName);
                    var uD = new UnitDealer(DbConnectionStringName);

                    var schedulersThatShouldBeUsed = sD.GetSchedulersBy(searchParameters.cronExpressions.ToList());
                    var unitIdToUse = int.Parse(searchParameters.unitId);
                    var currentUnit = uD.UnitBy(unitIdToUse);
                    var actionTypeOption = aTD.ActionTypeOptionBy(searchParameters.actionTypeOption);
                    var currentActionType = aTD.GetActionTypeBy(actionTypeOption);
                    
                    var schedulersIdsList = (from s in schedulersThatShouldBeUsed
                                             select s.Id).ToList();
                    var availableActionsThatCanBeUsed = (from a in db.TellstickActions
                                                        where a.TellstickUnit_Id == unitIdToUse && schedulersIdsList.Contains((int)a.Scheduler_Id) && a.TellstickActionType.ActionTypeOption == actionTypeOption
                                                        select a).ToList();

                    var availableActionsSchedulers = new List<Scheduler>();

                    foreach (var action in availableActionsThatCanBeUsed)
                    {
                        //make sure to activate possible inactive actions
                        action.Active = true;
                        availableActionsSchedulers.Add(action.Scheduler);
                    }

                    //missing actions, which do we need to create?
                    var schedulersWeNeedToCreateActionsFor = (from needed in schedulersThatShouldBeUsed
                                                             where !(
                                                                 from available in availableActionsSchedulers
                                                                 select available.CronExpression
                                                             ).Contains(needed.CronExpression)
                                                             select needed).ToList();

                    //create new actions which we miss
                    foreach (var scheduler in schedulersWeNeedToCreateActionsFor)
                    {
                        var newAction = new Core.Model.TellstickAction
                        {
                            Active = true,
                            TellstickActionType_Id = currentActionType.Id,
                            Scheduler_Id = scheduler.Id,
                            TellstickUnit_Id = currentUnit.Id
                        };

                        db.TellstickActions.Add(newAction);

                    }

                    // Submit the changes to the database.
                    try
                    {
                        db.SaveChanges();
                        var allActionsForCurrentUnit = (from action in db.TellstickActions
                            where action.TellstickUnit.Id == unitIdToUse
                            select action).ToList();
                        return allActionsForCurrentUnit;
                    }
                    catch (Exception ex)
                    {
                        log.Error("Cold not ActivateActionsFor ", ex);
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                

            }
        }
    }
}
