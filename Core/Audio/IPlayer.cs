using Core.Model.Interfaces;

namespace Core.Audio
{
    public interface IPlayer
    {
        int Volume { get; set; }
        void Play(IMediaSource mediaSource);
        void Play(IMediaSource mediaSource, IMediaOutputVolume mediaOutputVolume);
        void Stop();
    }
}