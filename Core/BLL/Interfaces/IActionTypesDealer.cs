using Core.Model.Enums;

namespace Core.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    interface IActionTypesDealer
    {
        Core.Model.TellstickActionType GetActionTypeBy(TellstickActionTypeOption actionTypeOption);

        TellstickActionTypeOption ActionTypeOptionBy(string name);
    }
}
