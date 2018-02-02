namespace Core.Model.ViewModel
{
    public interface IRegisteredMediaSource
    {
        string Name { get; set; }
        string Url { get; set; }
        Enums.MediaCategoryType MediaCategoryType { get; set; }
    }
}