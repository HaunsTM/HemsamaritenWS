namespace Tellstick.Model.Enums
{
    using System.ComponentModel;

    public enum EnumTellstickModelType
    {
        [Description("bell")]
        bell = 0,
        [Description("code-switch")]
        codeswitch = 1,
        [Description("selflearning")]
        selflearning = 2 ,
        [Description("selflearning-dimmer")]
        selflearningDimmer = 3,
        [Description("selflearning-switch")]
        selflearningSwitch = 4
    }
}