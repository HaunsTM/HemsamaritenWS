using Tellstick.Model.Enums;

namespace Tellstick.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using Tellstick.BLL.Interfaces;
    using Tellstick.Model;

    using Action = Tellstick.Model.Action;

    public class PerformedActionsDealer : IPerformedActionsDealer
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

        public Tellstick.Model.ViewModel.LastPerformedTellstickAction LastPerformedAction(string name)
        {
            var lastPerformedAction = new Model.ViewModel.LastPerformedTellstickAction();

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var performedAction = (from performedAct in db.PerformedActions
                        where performedAct.Action.Unit.Name == name
                        orderby performedAct.Time descending
                        select performedAct).FirstOrDefault();

                    lastPerformedAction.Performed = performedAction.Time;
                    lastPerformedAction.NameOfPerfomee = name;
                    lastPerformedAction.NameOfPerformedAction = performedAction.Action.ActionType.ActionTypeOption;
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not retrieve performed actions from database!", ex);
                throw ex;
            }
            return lastPerformedAction;
        }


        public List<Model.ViewModel.LastPerformedTellstickAction> LastPerformedActionsForAllUnits()
        {
            var lastPerformedActions = new List<Model.ViewModel.LastPerformedTellstickAction>();

            try
            {
                using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var query = db.PerformedActions
                        .GroupBy(element => element.Action.Unit_Id)
                        .Select(groups => groups.OrderByDescending(p => p.Time)
                        .FirstOrDefault())
                        .Select( x => new Model.ViewModel.LastPerformedTellstickAction
                        {
                            Performed = x.Time,
                            NameOfPerfomee = x.Action.Unit.Name,
                            NameOfPerformedAction = x.Action.ActionType.ActionTypeOption
                        });

                    lastPerformedActions = query.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not retrieve performed actions from database!", ex);
                throw ex;
            }
            return lastPerformedActions;
        }
    }
}