using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Core.BLL.Interfaces;
using Core.Model;
using Core.Model.Enums;
using Core.Model.Interfaces;
using log4net;

namespace Core.BLL
{

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
        public bool Register(Model.Action occurredAction, DateTime timeOfOccurrence)
        {
            var registered = false;

            try
            {
                using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
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
        /// Retrieves a list of occurred TellstickActions from database
        /// </summary>
        /// <param name="active">A flag that indicates if we should search for active items</param>
        /// <param name="startTime">A time stamp that indicates search start time</param>
        /// <param name="endTime">A time stamp that indicates search end time</param>
        public List<Core.Model.Interfaces.IAction> OccurredActions(bool active, DateTime startTime, DateTime endTime)
        {
            var occurredActions = new List<Core.Model.Interfaces.IAction>();

            try
            {
                using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
                {
                    occurredActions = (from performedAct in db.PerformedActions
                                      where (performedAct.Time >= startTime && performedAct.Time <= endTime && performedAct.Active == active)
                                      select performedAct.Action).ToList<Core.Model.Interfaces.IAction>();

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