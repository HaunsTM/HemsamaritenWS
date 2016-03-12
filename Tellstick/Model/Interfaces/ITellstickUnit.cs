namespace Tellstick.Model.Interfaces
{
    public interface ITellstickUnit : IEntity
    {
        string Name { get; set; }
        string LocationDesciption { get; set; }
    }
}