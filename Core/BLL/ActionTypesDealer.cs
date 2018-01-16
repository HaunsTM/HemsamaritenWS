
using Core.Model.Enums;

namespace Core.BLL
{
    using System.Linq;
    using log4net;

    using Core.BLL.Interfaces;

    public class ActionTypesDealer : IActionTypesDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public ActionTypesDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public Core.Model.TellstickActionType GetActionTypeBy(ActionTypeOption actionTypeOption)
        {
            using (var db = new Core.Model.TellstickDBContext(DbConnectionStringName))
            {
                var actionType = from a in db.TellstickActionTypes
                    where a.ActionTypeOption == actionTypeOption
                                  select a;

                return actionType.FirstOrDefault();
            }
        }

        public ActionTypeOption ActionTypeOptionBy(string name)
        {
            var actionTypeOption = (ActionTypeOption)System.Enum.Parse(typeof(ActionTypeOption), name);
            return actionTypeOption;
        }
    }
}
