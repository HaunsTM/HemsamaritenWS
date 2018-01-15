namespace Core.Model.Interfaces
{
    public interface IActionType : IEntity
    {
        Core.Model.Enums.ActionTypeOption ActionTypeOption { get; set; }
    }
}