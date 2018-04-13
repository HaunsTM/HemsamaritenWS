using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.BLL.Interfaces;
using Core.Model;
using Core.Model.ViewModel;
using log4net;
using MoreLinq;

namespace Core.BLL
{
    public class SchedulerDealer : ISchedulerDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public SchedulerDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public Scheduler GetSchedulerBy(string cronExpression)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var scheduler = from s in db.Schedulers
                                 where s.CronExpression == cronExpression
                                 select s;

                return scheduler.FirstOrDefault();
            }
        }

        public IEnumerable<Scheduler> GetSchedulersBy(List<string> cronExpressions)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var schedulers =  from s in db.Schedulers
                                  where cronExpressions.Contains(s.CronExpression)
                                  select s;

                return schedulers.ToList();
            }
        }

        private string TimeOccurranceIgnoringDay(string entireCronexpression)
        {
            var match = new Regex(@"(?<timePart>(\d+\s){3}).*").Match(entireCronexpression);
            if (match.Success)
            {
                return match.Groups["timePart"].Value;
            }

            return "";
        }

        public List<TellsticksSchedulerActionTypeOption> GetTellsticksWithTheirSchedulersSplitOnActions()
        {
            /*  Example output:
             *
             * {TellstickUnit_Id: 14,
             * TellstickActionType_Id: 2,
             * crons expression: [
             * "0 0 23 * * ?",
             * "0 45 8 * * ?"
             * ]},
             * 
             * TellstickUnit_Id: 15,
             * TellstickActionType_Id: 1,
             * crons expression: [
             * "0 45 5 * * ?",
             * "0 0 16 * * ?"
             * ]}
             */

            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var tellsticksWithTheirSchedulersSplitOnActions = db.Actions.OfType<TellstickAction>()
                    .Where(a => a.Active)
                    .GroupBy(t => new {t.TellstickUnit_Id, t.TellstickActionType_Id})
                    .OrderBy(t => t.Key.TellstickUnit_Id).ThenBy(t => t.Key.TellstickActionType_Id)
                    .Select(g => new TellsticksSchedulerActionTypeOption
                    {
                        TellstickUnit_Id = g.Key.TellstickUnit_Id,
                        TellstickActionType_Id = g.Key.TellstickActionType_Id,
                        CronExpression = g.Select(s => s.Scheduler.CronExpression).ToList()

                    }).ToList();

                return tellsticksWithTheirSchedulersSplitOnActions;
            }
        }

        public List<SchedulersTellsticksActionTypeOption> GetSchedulersUsingTellsticksSplitOnActions()
        {
            /* Output:
             * CronExpression: 0 58 2 * * ?
             *      TellstickActionType_Id: 3
             *      TellstickUnit_Ids:        
             *
             *  CronExpression: 0 45 5 * * ?
             *      TellstickActionType_Id: 1
             *      TellstickUnit_Ids: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
             *
             *  CronExpression: 0 46 5 * * ?
             *      TellstickActionType_Id: 1
             *      TellstickUnit_Ids: 13
             *
             *  CronExpression: 0 47 5 * * ?
             *      TellstickActionType_Id: 1
             *      TellstickUnit_Ids: 14, 15, 16
             */
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {

                var schedulersUsingTellsticksSplitOnActions = db.Actions.OfType<TellstickAction>()
                    .Where(a => a.Active)
                    .GroupBy(t => new {t.Scheduler_Id, t.TellstickActionType_Id})
                    .OrderBy(t => t.Key.Scheduler_Id).ThenBy(t => t.Key.TellstickActionType_Id)
                    .Select(g => new SchedulersTellsticksActionTypeOption
                    {
                        TellstickActionType_Id = g.Key.TellstickActionType_Id,
                        CronExpression = g.Where(c => c.Scheduler_Id == g.Key.Scheduler_Id).Select(c => c.Scheduler.CronExpression).FirstOrDefault(),
                        TellstickUnit_Ids = db.Actions.OfType<TellstickAction>()
                            .Where(tA => tA.TellstickActionType_Id == g.Key.TellstickActionType_Id)
                            .Where(s => s.Scheduler.CronExpression == g.Where(c => c.Scheduler_Id == g.Key.Scheduler_Id)
                                            .Select(c => c.Scheduler.CronExpression).FirstOrDefault())
                            .Select(i => i.TellstickUnit_Id).ToList()
                    })
                    .ToList();
                
                return schedulersUsingTellsticksSplitOnActions;
            }
        }
    }
}
