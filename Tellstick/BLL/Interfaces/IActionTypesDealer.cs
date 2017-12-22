using Tellstick.Model.Enums;

namespace Tellstick.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IActionTypesDealer
    {
        Tellstick.Model.ActionType GetActionTypeBy(ActionTypeOption actionTypeOption);

        ActionTypeOption ActionTypeOptionBy(string name);
    }
}
