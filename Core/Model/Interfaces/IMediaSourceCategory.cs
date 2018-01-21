namespace Core.Model.Interfaces
{
    public interface IMediaSourceCategory : IEntity
    {
        Core.Model.Enums.MediaCategoryType MediaCategoryType { get; set; }
    }
}