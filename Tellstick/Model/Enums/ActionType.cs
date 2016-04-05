namespace Tellstick.Model.Enums
{
    using System.ComponentModel;

    public enum ActionType
    {
        [Description("Turn on")]
        TurnOn,
        [Description("Turn off")]
        TurnOff,
        [Description("Dim")]
        Dim
    }
}