namespace Core.Model.Interfaces
{
    public interface ITellstickUnit : IEntity
    {
        int NativeDeviceId { get; set; }
        string Name { get; set; }
        string LocationDesciption { get; set; }
    }
}