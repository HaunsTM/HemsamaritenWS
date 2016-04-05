namespace Tellstick.Model.Interfaces
{
    public interface IProtocol : IEntity
    {
        string Name { get; }
        Model.Enums.Protocol Type { get; set; }
    }
}