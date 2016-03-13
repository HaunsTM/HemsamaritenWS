namespace Tellstick.Model.Interfaces
{
    public interface ITellstickActionType
    {
        Model.Enums.EnumTellstickActionType Type { get; set; }
        int DimValue { get; set; }
    }
}