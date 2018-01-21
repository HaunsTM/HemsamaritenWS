namespace Core.Model.Interfaces
{
    public interface IMediaSource : IEntity
    {
        string Url { get; set; }
        string MediaDataBase64 { get; set; }
    }
}