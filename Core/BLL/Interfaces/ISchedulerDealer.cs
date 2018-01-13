using System.Collections.Generic;
using System.Linq;

namespace Tellstick.BLL.Interfaces
{
    using Tellstick.Model;

    interface ISchedulerDealer
    {
        string DbConnectionStringName { get; }

        Scheduler GetSchedulerBy(string cronExpression);
        IEnumerable<Scheduler> GetSchedulersBy(List<string> cronExpressions);
    }
}
