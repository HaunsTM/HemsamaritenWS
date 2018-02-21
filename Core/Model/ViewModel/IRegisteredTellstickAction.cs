using System;

namespace Core.Model.ViewModel
{
    public interface IRegisteredTellstickAction
    {
        int Action_Id { get; set; }
        string LastPerformedTimeUTC { get; set; }

        int Scheduler_Id { get; set; }
        
        string CronDescription { get; set; }
        string CronExpression { get; set; }
        int TellstickActionType_Id { get; set; }
        int TellstickUnit_Id { get; set; }
    }
}