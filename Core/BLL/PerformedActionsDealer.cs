using System.ComponentModel;
using Core.Model.Enums;

namespace Core.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using Core.BLL.Interfaces;
    using Core.Model;

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
        public bool Register(Core.Model.TellstickAction occurredAction, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
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
                using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
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
        public List<Core.Model.TellstickAction> OccurredTellstickActions(bool active, DateTime startTime, DateTime endTime)
        {
            var occurredTellstickActions = new List<TellstickAction>();

            try
            {
                using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
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

        public Core.Model.ViewModel.LastPerformedTellstickAction LastPerformedAction(string name)
        {
            var lastPerformedAction = new Model.ViewModel.LastPerformedTellstickAction();

            try
            {
                using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var performedAction = (from performedAct in db.PerformedActions
                        where performedAct.Action.TellstickUnit.Name == name
                        orderby performedAct.Time descending
                        select performedAct).FirstOrDefault();

                    lastPerformedAction.Time = performedAction.Time;
                    lastPerformedAction.Name = name;
                    lastPerformedAction.PerformedActionDescription = performedAction.Action.TellstickActionType.ActionTypeOption.GetAttributeOfType<DescriptionAttribute>().Description;
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
                using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
                {
                    var query = db.PerformedActions
                        .GroupBy(element => element.Action.TellstickUnit_Id)
                        .Select(groups => groups.OrderByDescending(p => p.Time)
                        .FirstOrDefault())
                        .Select( x => new
                        {
                            Time = x.Time,
                            Name = x.Action.TellstickUnit.Name,
                            PerformedActionTypeOption = x.Action.TellstickActionType.ActionTypeOption
                        });

                    var allLastPerformedActions = query.ToList();

                    var allLastPerformedActionsWithDescription = allLastPerformedActions
                        .Select( x => new Model.ViewModel.LastPerformedTellstickAction
                            {
                                Time = x.Time,
                                Name = x.Name,
                                PerformedActionDescription = x.PerformedActionTypeOption.GetAttributeOfType<DescriptionAttribute>().Description
                        })
                        .ToList();

                    lastPerformedActions = allLastPerformedActionsWithDescription;
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