namespace Core.Model.Interfaces
{
    public interface IMediaOutput: IEntity
    {
        Enums.MediaOutputTargetType Target { get; set; }
    }
}