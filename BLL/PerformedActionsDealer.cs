namespace Tellstick.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using Tellstick.Model;

    public class PerformedActionsDealer
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
        public bool Register(Tellstick.Model.TellstickAction occurredAction, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Model.TellstickDBContext(DbConnectionStringName))
                {
                    var pa = new PerformedAction() { Active = true, TellstickAction = occurredAction, Time = timeOfOccurrence };
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
        public bool Register(int occurredTellstickAction_Id, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Model.TellstickDBContext(DbConnectionStringName))
                {
                    var pa = new PerformedAction() { Active = true, TellstickAction_Id = occurredTellstickAction_Id, Time = timeOfOccurrence };
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
        public List<Model.TellstickAction> OccurredTellstickActions(bool active, DateTime startTime, DateTime endTime)
        {
            var occurredTellstickActions = new List<TellstickAction>();

            try
            {
                using (var db = new Model.TellstickDBContext(DbConnectionStringName))
                {
                    var queryResult = from performedAct in db.PerformedActions
                                      where (performedAct.Time >= startTime && performedAct.Time <= endTime && performedAct.Active == active)
                                      select new
                                      {
                                          performedAct.TellstickAction
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

    }
}