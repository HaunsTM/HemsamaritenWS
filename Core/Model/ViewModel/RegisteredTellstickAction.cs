using System;

namespace Core.Model.ViewModel
{
    public class RegisteredTellstickAction : IRegisteredTellstickAction
    {
        public int Action_Id { get; set; }
        public string LastPerformedTimeUTC { get; set; }

        public int Scheduler_Id { get; set; }

        public string CronDescription { get; set; }
        public string CronExpression { get; set; }
        public int TellstickActionType_Id { get; set; }
        public int TellstickUnit_Id { get; set; }
    }   
}