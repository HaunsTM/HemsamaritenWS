using System.ComponentModel;

namespace Core.Model.Enums
{
    public enum ActionTypeOption
    {
        [Description("Turn on")]
        TurnOn,
        [Description("Turn off")]
        TurnOff,
        [Description("Refresh bearer token")]
        RefreshBearerToken
    }
}