namespace Core.Model.ViewModel
{
    public class RegisteredMediaSource : IRegisteredMediaSource
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Enums.MediaCategoryType MediaCategoryType { get; set; }
    }   
}