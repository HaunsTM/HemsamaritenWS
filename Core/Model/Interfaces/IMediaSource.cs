namespace Core.Model.Interfaces
{
    public interface IMediaSource : IEntity
    {
        string Name { get; set; }
        string Url { get; set; }
        string MediaDataBase64 { get; set; }

        Core.Model.Enums.MediaCategoryType MediaCategoryType { get; set; }
    }
}