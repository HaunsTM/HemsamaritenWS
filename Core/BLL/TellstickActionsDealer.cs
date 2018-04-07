﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Core.BLL.Interfaces;
using Core.Model;
using Core.Model.Enums;
using Core.Model.Interfaces;
using Core.Model.ViewModel;
using log4net;
using MoreLinq;

namespace Core.BLL
{

    public class ActionSearchParameters : ITellstickActionSearchParameters
    {
        public string UnitId { get; set; }
        public string ActionTypeOption { get; set; }
        public string[] CronExpressions { get; set; }
    }

    public class TellstickActionsDealer : ITellstickActionsDealer
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
                        actionToSearchFor = (from existingAction in db.Actions.OfType<TellstickAction>()
                                             where
                                                 existingAction.Active == true
                                                 && existingAction.TellstickUnit.Id == currentUnit.Id
                                                 && existingAction.TellstickActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;
                    case false:
                        actionToSearchFor = (from existingAction in db.Actions.OfType<TellstickAction>()
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
        
        public Core.Model.TellstickAction RegisterNewManualAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Scheduler scheduler = null)
        {
            try
            {
                using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
                {
                    //which ActionType are we dealing with?
                    var currentActionType = (from actionType in db.TellstickActionTypes
                                             where actionType.Active && actionType.ActionTypeOption == actionTypeOption
                                             select actionType).First();

                    //which tellstick Unit are we dealing with?
                    var currentUnit = (from unit in db.TellstickUnits where unit.NativeDeviceId == nativeDeviceId select unit).First();

                    var newManualAction = new Core.Model.TellstickAction
                    {
                        Active = true,
                        Scheduler_Id = scheduler?.Id,
                        TellstickActionType_Id = currentActionType.Id,
                        TellstickUnit_Id = currentUnit.Id
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

        public Core.Model.TellstickAction RegisterNewManualAction(string name, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Scheduler scheduler = null)
        {
            TellstickUnit currentUnit;

            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                //which tellstick Unit are we dealing with?
                currentUnit = (from unit in db.TellstickUnits
                    where unit.Name == name
                    select unit).FirstOrDefault();
            }

            return this.RegisterNewManualAction(nativeDeviceId: currentUnit.NativeDeviceId, actionTypeOption: actionTypeOption, scheduler: scheduler);
        }

        public List<TellsticksSchedulerActionTypeOption> GetAllActions()
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                
                var schedulersWithActions = db.Actions.OfType<TellstickAction>()
                    .Where(a => a.Active).DistinctBy(s => new { s.Scheduler_Id, s.TellstickActionType_Id}).ToList()
                    .Select( s => new { tellstickAction_Id = s.Id, scheduler_Id = s.Scheduler_Id, cronExpression = s.Scheduler.CronExpression, tellstickActionType_Id = s.TellstickActionType_Id} ).ToList();

                //which tellsticks share the same shedulers and actiontypes?
                var shedulersSharingActions = schedulersWithActions.Select(s => new TellsticksSchedulerActionTypeOption
                {
                    TellstickActionId = s.tellstickAction_Id,
                    LastPerformedTime = db.PerformedActions.Where( p => p.Action.Id == s.tellstickAction_Id ).OrderBy( t => t.Time).Select(t => t.Time).Take(1).FirstOrDefault(),
                    Scheduler_Id = s.scheduler_Id,
                    TellstickActionType_Id = s.tellstickActionType_Id,
                    CronExpression = s.cronExpression,
                    TellstickUnit_Ids = db.Actions.OfType<TellstickAction>()
                        .Where(a => a.Scheduler_Id == s.scheduler_Id).Select(t => t.TellstickUnit_Id).ToList()
                }).ToList();
                return shedulersSharingActions;
            }
        }
        
        public List<Core.Model.TellstickAction> ActivateActionsFor(ITellstickActionSearchParameters searchParameters)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext( DbConnectionStringName ) )
            {
                try
                {
                    var sD = new SchedulerDealer(DbConnectionStringName);
                    var aTD = new ActionTypesDealer(DbConnectionStringName);
                    var uD = new UnitDealer(DbConnectionStringName);

                    var schedulersThatShouldBeUsed = sD.GetSchedulersBy(searchParameters.CronExpressions.ToList());
                    var unitIdToUse = int.Parse(searchParameters.UnitId);
                    var currentUnit = uD.UnitBy(unitIdToUse);
                    var actionTypeOption = aTD.TellstickActionTypeOptionBy(searchParameters.ActionTypeOption);
                    var currentActionType = aTD.GetTellstickActionTypeBy(actionTypeOption);
                    
                    var schedulersIdsList = (from s in schedulersThatShouldBeUsed
                                             select s.Id).ToList();
                    var availableActionsThatCanBeUsed = (from a in db.Actions.OfType<TellstickAction>()
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

                        db.Actions.Add(newAction);

                    }

                    // Submit the changes to the database.
                    try
                    {
                        db.SaveChanges();
                        var allActionsForCurrentUnit = (from action in db.Actions.OfType<TellstickAction>()
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

        public Core.Model.TellstickAction AddAction(int nativeDeviceId, Core.Model.Enums.TellstickActionTypeOption actionTypeOption, Scheduler scheduler)
        {
            Core.Model.TellstickAction addedAction = null;
            try
            {
                //do we have an Action in db for this event already?
                var possibleRegisteredAction = this.ActionExists(nativeDeviceId: nativeDeviceId, actionTypeOption: actionTypeOption, scheduler: scheduler);
                
                if (possibleRegisteredAction == null)
                {
                    //no we haven't, register an new Action
                    addedAction = this.RegisterNewManualAction(nativeDeviceId: nativeDeviceId, actionTypeOption: actionTypeOption, scheduler: scheduler);
                }
                else if (!possibleRegisteredAction.Active)
                {
                    //yes, but the the Action had Active = false
                    using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
                    {
                        var currentActionFromDatabase = db.Actions.SingleOrDefault(a => a.Id == possibleRegisteredAction.Id);
                        currentActionFromDatabase.Active = true;
                        db.SaveChanges();
                        addedAction = (Core.Model.TellstickAction)currentActionFromDatabase;
                    }
                }
                else if (possibleRegisteredAction.Active)
                {
                    //yes, and the Action hade it's flag set to Active = false
                    addedAction = possibleRegisteredAction;
                }

                return addedAction;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Core.Model.TellstickAction AddAction(int tellstickUnitId,
            Core.Model.Enums.TellstickActionTypeOption actionTypeOption, string schedulerCronExpression)
        {
            Scheduler currentScheduler = null;
            TellstickUnit currentTellsticUnit = null;
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                try
                {
                    var currentSchedulers = db.Schedulers.Where(s => s.Active && s.CronExpression == schedulerCronExpression).ToList();
                    currentScheduler = db.Schedulers.Where(s => s.Active && s.CronExpression == schedulerCronExpression).Single();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Invalid cron expression: {schedulerCronExpression}", ex);
                }
                currentTellsticUnit = db.TellstickUnits.Where(t => t.Id == tellstickUnitId).Single();
            }
            
            return AddAction(nativeDeviceId: currentTellsticUnit.NativeDeviceId, actionTypeOption: actionTypeOption,
                scheduler: currentScheduler);
        }

        public bool RemoveAction(int actionId)
        {
            var removed = false;
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(this.DbConnectionStringName))
            {
                var currentActionFromDatabase = db.Actions.SingleOrDefault(a => a.Id == actionId);
                currentActionFromDatabase.Active = false;
                db.SaveChanges();
                removed = true;
            }
            return removed;
        }
    }
}
