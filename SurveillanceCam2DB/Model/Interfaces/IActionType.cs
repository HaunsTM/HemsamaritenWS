namespace Tellstick.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        Enums.ActionTypes Type { get; set; }
    }
}