using System.Collections.Generic;
using Core.Model.ViewModel;

namespace Core.BLL.Interfaces
{
    using Core.Model;

    interface ISchedulerDealer
    {
        string DbConnectionStringName { get; }

        Scheduler GetSchedulerBy(string cronExpression);
        IEnumerable<Scheduler> GetSchedulersBy(List<string> cronExpressions);

        List<TellsticksSchedulerActionTypeOption> GetTellsticksWithTheirSchedulersSplitOnActions();
        List<SchedulersTellsticksActionTypeOption> GetSchedulersUsingTellsticksSplitOnActions();
    }
}
