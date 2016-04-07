namespace Tellstick.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        Tellstick.Model.Enums.ActionTypeOption ActionTypeOption { get; set; }
    }
}