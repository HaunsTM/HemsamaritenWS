using System.Collections.Generic;
using System.Linq;

namespace Core.BLL.Interfaces
{
    using Core.Model;

    interface ISchedulerDealer
    {
        string DbConnectionStringName { get; }

        Scheduler GetSchedulerBy(string cronExpression);
        IEnumerable<Scheduler> GetSchedulersBy(List<string> cronExpressions);
    }
}
