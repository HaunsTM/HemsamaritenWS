namespace Tellstick.Model.Interfaces
{
    public interface ITellstickParameter : IEntity
    {
        Model.Enums.TellstickParameter_House House { get; set; }
        Model.Enums.TellstickParameter_Unit Unit { get; set; }
    }
}