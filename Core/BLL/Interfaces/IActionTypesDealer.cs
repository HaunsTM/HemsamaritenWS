using Core.Model.Enums;

namespace Core.BLL.Interfaces
{
    interface IActionTypesDealer
    {
        Core.Model.TellstickActionType GetTellstickActionTypeBy(TellstickActionTypeOption actionTypeOption);

        TellstickActionTypeOption TellstickActionTypeOptionBy(string name);
    }
}
