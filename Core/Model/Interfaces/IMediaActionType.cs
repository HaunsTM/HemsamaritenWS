namespace Core.Model.Interfaces
{
    public interface IMediaActionType : IEntity
    {
        Enums.MediaActionTypeOption ActionTypeOption { get; set; }
    }
}
