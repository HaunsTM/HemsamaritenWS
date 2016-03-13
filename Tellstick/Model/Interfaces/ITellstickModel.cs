namespace Tellstick.Model.Interfaces
{
    public interface ITellstickModelTypeAndTellstickManufacturer
    {
        Enums.EnumTellstickModelType Type { get; set; }
        Enums.EnumTellstickModelManufacturer Manufacturer { get; set; }
    }

    public interface ITellstickModel : IEntity, ITellstickModelTypeAndTellstickManufacturer
    {
        string Model { get; }
    }
}