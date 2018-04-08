using System;
using System.Collections.Generic;

namespace Core.Model.ViewModel
{
    public class TellsticksSchedulerActionTypeOption
    {
        public List<string> CronExpression { get; set; }
        public int? TellstickActionType_Id { get; set; }
        public int? TellstickUnit_Id { get; set; }
    }   
}