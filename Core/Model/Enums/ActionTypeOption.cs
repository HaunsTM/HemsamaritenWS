namespace Tellstick.Model.Enums
{
    using System.ComponentModel;

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