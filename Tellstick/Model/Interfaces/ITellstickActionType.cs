namespace Tellstick.Model.Interfaces
{
    public interface ITellstickActionType : IEntity
    {
        Model.Enums.EnumTellstickActionType Type { get; set; }
        int DimValue { get; set; }
    }
}