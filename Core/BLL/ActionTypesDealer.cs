using System.Linq;
using Core.BLL.Interfaces;
using Core.Model.Enums;
using log4net;

namespace Core.BLL
{
    public class ActionTypesDealer : IActionTypesDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public ActionTypesDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public Core.Model.TellstickActionType GetActionTypeBy(TellstickActionTypeOption actionTypeOption)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                var actionType = from a in db.TellstickActionTypes
                    where a.ActionTypeOption == actionTypeOption
                                  select a;

                return actionType.FirstOrDefault();
            }
        }

        public TellstickActionTypeOption ActionTypeOptionBy(string name)
        {
            var actionTypeOption = (TellstickActionTypeOption)System.Enum.Parse(typeof(TellstickActionTypeOption), name);
            return actionTypeOption;
        }
    }
}
