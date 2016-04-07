namespace Tellstick.Model.Interfaces
{
    public interface IModelTypeAndTellstickManufacturer : IEntity
    {
        Enums.ModelTypeOption TypeOption { get; set; }
        Enums.ModelManufacturerOption ManufacturerOption { get; set; }
    }

    public interface IModel : IEntity, IModelTypeAndTellstickManufacturer
    {
        string Description { get; }
    }
}