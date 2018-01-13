
using Tellstick.Model.Enums;

namespace Tellstick.BLL
{
    using System.Linq;
    using log4net;

    using Tellstick.BLL.Interfaces;

    public class ActionTypesDealer : IActionTypesDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public ActionTypesDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        public Tellstick.Model.ActionType GetActionTypeBy(ActionTypeOption actionTypeOption)
        {
            using (var db = new Tellstick.Model.TellstickDBContext(DbConnectionStringName))
            {
                var actionType = from a in db.ActionTypes
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
