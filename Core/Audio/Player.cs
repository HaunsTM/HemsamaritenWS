using System;
using System.ComponentModel.DataAnnotations;
using Core.Model.Interfaces;

namespace Core.Audio
{
    public class Player : IPlayer
    {
        //https://msdn.microsoft.com/en-us/library/windows/desktop/dd562692(v=vs.85).aspx
        private WMPLib.WindowsMediaPlayer _mediaPlayer;
        private ISystemVolumeConfigurator _windowsNativeAudioSystem;

        public Player(WMPLib.WindowsMediaPlayer mediaPlayer, ISystemVolumeConfigurator windowsNativeAudioSystem)
        {
            _mediaPlayer = mediaPlayer;
            _windowsNativeAudioSystem = windowsNativeAudioSystem;

            SetInitilalSettings();
        }

        private void SetInitilalSettings()
        {
            SetNativeVolumeControlToMax();
        }

        private void SetNativeVolumeControlToMax()
        {
            _windowsNativeAudioSystem.Volume = 100;
        }

        [Range(0, 100,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Volume
        {
            get { return _mediaPlayer.settings.volume; }
            set { _mediaPlayer.settings.volume = value; }
        }

        private void Play(string url)
        {
            _mediaPlayer.URL = url;
            _mediaPlayer.controls.play();
        }

        public void Play(IMediaSource mediaSource)
        {
            if (mediaSource.MediaDataBase64 != null)
            {
                throw new NotImplementedException("Playing media from data in base 64 format is not yet implemented");
            }
            var url = mediaSource.Url;
            this.Play(url);
        }

        public void Play(IMediaSource mediaSource, IMediaOutputVolume mediaOutputVolume)
        {
            this.Volume = mediaOutputVolume.Value;

            this.Play(mediaSource);
        }

        public void Stop()
        {
            _mediaPlayer.controls.stop();
        }
    }
}
 