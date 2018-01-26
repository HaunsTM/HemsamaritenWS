namespace Core.Model.Interfaces
{
    public interface ITellstickActionType : IEntity
    {
        Core.Model.Enums.TellstickActionTypeOption ActionTypeOption { get; set; }
    }
}