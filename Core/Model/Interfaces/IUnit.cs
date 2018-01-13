namespace Tellstick.Model.Interfaces
{
    public interface IUnit : IEntity
    {
        int NativeDeviceId { get; set; }
        string Name { get; set; }
        string LocationDesciption { get; set; }
    }
}