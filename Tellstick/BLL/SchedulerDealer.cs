namespace Tellstick.BLL
{
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using Tellstick.BLL.Interfaces;
    using Tellstick.Model;

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
            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                var scheduler = from s in db.Schedulers
                                 where s.CronExpression == cronExpression
                                 select s;

                return scheduler.FirstOrDefault();
            }
        }

        public IEnumerable<Scheduler> GetSchedulersBy(List<string> cronExpressions)
        {
            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                var schedulers =  from s in db.Schedulers
                                  where cronExpressions.Contains(s.CronExpression)
                                  select s;

                return schedulers.ToList();
            }
        }
    }
}
