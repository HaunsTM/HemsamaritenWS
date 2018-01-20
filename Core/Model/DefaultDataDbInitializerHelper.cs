using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Enums;

namespace Core.Model
{
    public sealed class DefaultDataDbInitializerHelper
    {
        public HemsamaritenWindowsServiceDbContext _context;

        public DefaultDataDbInitializerHelper(HemsamaritenWindowsServiceDbContext context)
        {
            _context = context;
        }

        public TellstickAction TellstickActionToSaveAsync(string tellstickUnitName, ActionTypeOption actionTypeOption, string cronExpression)
        {
            var tellstickAction = new TellstickAction();

            var scheduler = _context.Schedulers
                .Where(s => s.Active)
                .Where(s => s.CronExpression == cronExpression)
                .FirstOrDefault<Scheduler>();

            var tellstickActionType= _context.TellstickActionTypes
                .Where(s => s.Active)
                .Where(s => s.ActionTypeOption == actionTypeOption)
                .FirstOrDefault<TellstickActionType>();

            var tellstickUnit = _context.TellstickUnits
                .Where(u => u.Active)
                .Where(u => u.Name == tellstickUnitName)
                .FirstOrDefault<TellstickUnit>();

            tellstickAction.Scheduler = scheduler;
            tellstickAction.TellstickActionType = tellstickActionType;
            tellstickAction.TellstickUnit = tellstickUnit;

            return tellstickAction;
        }


        public List<TellstickAction> TellstickActionsToSaveAsync(List<Tuple<string, ActionTypeOption, string>> tellstickActionData)
        {
            var tellstickActionsToSave = new List<TellstickAction>();
            foreach (var data in tellstickActionData)
            {
                var tellstickUnitName = data.Item1;
                var actionTypeOption = data.Item2;
                var cronExpression = data.Item3;

                var tellstickActionToSave =  TellstickActionToSaveAsync(tellstickUnitName: tellstickUnitName,
                    actionTypeOption: actionTypeOption, cronExpression: cronExpression);

                tellstickActionsToSave.Add(tellstickActionToSave);
            }
            return tellstickActionsToSave;
        }

    }
}
