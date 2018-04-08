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
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var shedulersSharingActions = db.Actions.OfType<TellstickAction>()
                    .Where(a => a.Active)
                    .GroupBy(t => new {t.TellstickUnit_Id, t.TellstickActionType_Id})
                    .OrderBy(t => t.Key.TellstickUnit_Id).ThenBy(t => t.Key.TellstickActionType_Id)
                    .Select(g => new TellsticksSchedulerActionTypeOption
                    {
                        TellstickUnit_Id = g.Key.TellstickUnit_Id,
                        TellstickActionType_Id = g.Key.TellstickActionType_Id,
                        CronExpression = g.Select(s => s.Scheduler.CronExpression).ToList()

                    }).ToList();

                foreach (var s in shedulersSharingActions)
                {
                    var output =
                        "TellstickUnit_Id: " + s.TellstickUnit_Id.ToString() + "\n" +
                        "    TellstickActionType_Id: " + s.TellstickActionType_Id.ToString() + "\n" +
                        "    crons expression: " + "\n";
                    foreach (var expr in s.CronExpression)
                    {
                        output += "        " + expr + "\n";
                    }

                    System.Diagnostics.Debug.WriteLine(output);
                }

                return shedulersSharingActions;
            }
        }

        public List<SchedulersTellsticksActionTypeOption> GetSchedulersWithUsingTellsticksSplitOnActions()
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {

                var shedulersSharingActions = db.Actions.OfType<TellstickAction>()
                    .Where(a => a.Active)
                    .GroupBy(t => new {t.Scheduler_Id, t.TellstickActionType_Id})
                    .OrderBy(t => t.Key.Scheduler_Id).ThenBy(t => t.Key.TellstickActionType_Id)
                    .Select(g => new SchedulersTellsticksActionTypeOption
                    {
                        TellstickUnit_Id = g.Select(t => t.TellstickUnit.Id).ToList(),
                        TellstickActionType_Id = g.Key.TellstickActionType_Id,
                        CronExpression = g.Where( c => c.Scheduler_Id == g.Key.Scheduler_Id).Select( c => c.Scheduler.CronExpression).FirstOrDefault()

                    }).ToList();

                foreach (var s in shedulersSharingActions)
                {
                    var output =
                        "CronExpression: " + s.CronExpression.ToString() + "\n" +
                        "    TellstickActionType_Id: " + s.TellstickActionType_Id.ToString() + "\n" +
                        "    TellstickUnit_Ids: " + "\n";
                    foreach (var expr in s.TellstickUnit_Id)
                    {
                        output += "        " + expr + "\n";
                    }

                    System.Diagnostics.Debug.WriteLine(output);
                }

                return shedulersSharingActions;
            }
        }
    }
}
