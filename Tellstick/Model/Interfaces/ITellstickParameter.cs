namespace Tellstick.Model.Interfaces
{
    public interface ITellstickParameter : IEntity
    {
        Model.Enums.EnumTellstickParameter_House House { get; set; }
        Model.Enums.EnumTellstickParameter_Unit Unit { get; set; }
    }
}