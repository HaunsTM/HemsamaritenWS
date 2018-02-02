namespace Core.BLL.Interfaces
{
    public interface IMediaDealer
    {
        void SetVolume(int value);
        void Play(string url);
        void Play(string url, int mediaOutputVolume);
        void Stop();
    }
}