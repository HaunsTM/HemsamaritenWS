namespace Tellstick.Model.Interfaces
{
    public interface IModelTypeAndTellstickManufacturer : IEntity
    {
        Enums.ModelType Type { get; set; }
        Enums.ModelManufacturer Manufacturer { get; set; }
    }

    public interface IModelTypeAndTellstickModel : IEntity, IModelTypeAndTellstickManufacturer
    {
        string Model { get; }
    }
}