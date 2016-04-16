namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using SurveillanceCam2DB.BLL.Interfaces;
    using SurveillanceCam2DB.Model;

    public class PerformedActionsDealer : IPerformedActionsDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public PerformedActionsDealer(string dbConnectionStringName)
        {
            this.DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Registers an occurred Action to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        public bool Register(Model.Action occurredAction, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
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
        /// Registers an occurred Action to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        public bool Register(int occurredAction_Id, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new SurveillanceCam2DB.Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
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
        /// Retrieves a list of occurred Actions from database
        /// </summary>
        /// <param name="active">A flag that indicates if we should search for active items</param>
        /// <param name="startTime">A time stamp that indicates search start time</param>
        /// <param name="endTime">A time stamp that indicates search end time</param>
        public List<Model.Action> OccurredActions(bool active, DateTime startTime, DateTime endTime)
        {
            var occurredActions = new List<Model.Action> ();

            try
            {
                using (var db = new SurveillanceCam2DB.Model.SurveillanceCam2DBContext(this.DbConnectionStringName))
                {
                    var queryResult = from performedAct in db.PerformedActions
                                      where (performedAct.Time >= startTime && performedAct.Time <= endTime && performedAct.Active == active)
                                      select new
                                      {
                                          performedAct.Action
                                      };
                    foreach (var item in queryResult)
                    {
                        occurredActions.Add(item.Action);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Could not retrieve performed actions from database!", ex);
                throw ex;
            }
            return occurredActions;
        }

    }
}