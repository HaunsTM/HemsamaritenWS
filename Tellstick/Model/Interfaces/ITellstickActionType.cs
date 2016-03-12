namespace Tellstick.Model.Interfaces
{
    public interface ITellstickActionType
    {
         Model.Enums.TellstickActionType Action { get; set; }
        int DimValue { get; set; }
    }
}