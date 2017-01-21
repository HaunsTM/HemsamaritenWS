using Tellstick.BLL.Helpers;
using Tellstick.Model;

namespace Tellstick.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;
    
    using Action = Tellstick.Model.Action;

    public class PerformedActionsDealer : Tellstick.BLL.Interfaces.IPerformedActionsDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public PerformedActionsDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Registers an occurred TellstickAction to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        public bool Register(Tellstick.Model.Action occurredAction, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var pa = new PerformedAction() { Active = true, Action = occurredAction, Time = timeOfOccurrence };
                    db.PerformedActions.Add(pa);
                    db.SaveChanges();

                    registered = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not save performed action to database!", ex);
                throw ex;
            }
            return registered;
        }

        /// <summary>
        /// Registers an occurred TellstickAction to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        public bool Register(int occurredAction_Id, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var pa = new PerformedAction() { Active = true, Action_Id = occurredAction_Id, Time = timeOfOccurrence };
                    db.PerformedActions.Add(pa);
                    db.SaveChanges();

                    registered = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not save performed action to database!", ex);
                throw ex;
            }
            return registered;
        }

        /// <summary>
        /// Retrieves a list of occurred TellstickActions from database
        /// </summary>
        /// <param name="active">A flag that indicates if we should search for active items</param>
        /// <param name="startTime">A time stamp that indicates search start time</param>
        /// <param name="endTime">A time stamp that indicates search end time</param>
        public List<Tellstick.Model.Action> OccurredTellstickActions(bool active, DateTime startTime, DateTime endTime)
        {
            var occurredTellstickActions = new List<Action>();

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var queryResult = from performedAct in db.PerformedActions
                                      where (performedAct.Time >= startTime && performedAct.Time <= endTime && performedAct.Active == active)
                                      select new
                                      {
                                          TellstickAction = performedAct.Action
                                      };
                    foreach (var item in queryResult)
                    {
                        occurredTellstickActions.Add(item.TellstickAction);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not retrieve performed actions from database!", ex);
                throw ex;
            }
            return occurredTellstickActions;
        }
        
        public List<Tellstick.Model.ViewModel.UnitPerformedAction> LatestRegisteredAction(int[] unitIdList)
        {
            var latestTellstickActions = new List<Tellstick.Model.ViewModel.UnitPerformedAction>();

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var perAct = from pA in (from p in db.PerformedActions
                            where unitIdList.Contains(p.Action.Unit_Id) &&
                                    p.Active == true
                            select p)
                        group pA by pA.Action.Unit_Id
                        into groupedUnits
                        select groupedUnits.OrderBy(gU => gU.Action.Unit_Id).ThenByDescending(gU => gU.Time).FirstOrDefault();

                    var latestPerformedActionsList = perAct.ToList();

                    foreach (var latestAction in latestPerformedActionsList)
                    {
                        latestTellstickActions.Add(
                            new Tellstick.Model.ViewModel.UnitPerformedAction(
                                unitId: latestAction.Action.Unit.Id,
                                name: latestAction.Action.Unit.Name,
                                locationDesciption: latestAction.Action.Unit.LocationDesciption,

                                latestActionTimeUTC: new TimeConverter().ConvertToUnixTime(latestAction.Time),

                                latestSetActionType: latestAction.Action.ActionType.ActionTypeOption.ToString()
                            )
                        );
                    }

                    return latestTellstickActions;
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not retrieve latest performed actions from database!", ex);
                throw ex;
            }

        }

    }
}