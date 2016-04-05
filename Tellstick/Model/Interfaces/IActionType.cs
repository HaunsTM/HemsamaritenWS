namespace Tellstick.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        Model.Enums.ActionType Type { get; set; }
        int DimValue { get; set; }
    }
}