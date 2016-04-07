namespace Tellstick.Model.Interfaces
{
    public interface IProtocol : IEntity
    {
        string Name { get; }
        Tellstick.Model.Enums.ProtocolOption Type { get; set; }
    }
}