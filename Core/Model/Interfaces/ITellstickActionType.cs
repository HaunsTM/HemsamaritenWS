namespace Core.Model.Interfaces
{
    public interface ITellstickActionType : IEntity
    {
        Core.Model.Enums.ActionTypeOption ActionTypeOption { get; set; }
    }
}