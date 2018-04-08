using System;
using System.Collections.Generic;

namespace Core.Model.ViewModel
{
    public class SchedulersTellsticksActionTypeOption
    {
        public string CronExpression { get; set; }
        public int? TellstickActionType_Id { get; set; }
        public List<int> TellstickUnit_Id { get; set; }
    }   
}