namespace Tellstick.Model.Interfaces
{
    public interface ITellstickProtocol : IEntity
    {
        string Name { get; }
        Model.Enums.EnumTellstickProtocol Type { get; set; }
    }
}