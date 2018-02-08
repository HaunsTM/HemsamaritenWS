using System;
using System.ComponentModel.DataAnnotations;
using AudioSwitcher.AudioApi.CoreAudio;
using Core.Model.Interfaces;

namespace Core.Audio
{
    public sealed class Player
    {
        //https://msdn.microsoft.com/en-us/library/windows/desktop/dd562692(v=vs.85).aspx
        private WMPLib.WindowsMediaPlayer _mediaPlayer;
        private CoreAudioDevice _windowsNativeAudioSystem;

        private bool _paused;

        private Player()
        {
            _mediaPlayer = new WMPLib.WindowsMediaPlayer();
            _windowsNativeAudioSystem = new CoreAudioController().DefaultPlaybackDevice;
            
            SetInitilalSettings();
        }

        public static Player Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly Player instance = new Player();
        }

        private void SetInitilalSettings()
        {
            SetNativeVolumeControlToMax();
            _paused = false;
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
            private set { _mediaPlayer.settings.volume = value; }
        }

        public void Pause()
        {
            _mediaPlayer.controls.pause();
            _paused = true;

        }

        public void Play(string url)
        {
            _mediaPlayer.URL = url;
            _mediaPlayer.controls.play();
        }

        public void Play(string url, int mediaOutputVolume)
        {
            this.Volume = mediaOutputVolume;

            _mediaPlayer.URL = url;
            _mediaPlayer.controls.play();
        }

        public void Play(IMediaSource mediaSource)
        {
            if (mediaSource == null) throw new Exception("Trying to play but the expected mediaSource was null.");
            if (mediaSource.MediaDataBase64 != null)
            {
                throw new NotImplementedException("Playing media from data in base 64 format is not yet implemented");
            }
            var url = mediaSource.Url;
            this.Play(url);
        }

        public void Play(IMediaSource mediaSource, IMediaOutputVolume mediaOutputVolume)
        {
            if (mediaOutputVolume == null) throw new Exception("Trying to play but the expected mediaOutputVolume was null.");
            this.Volume = mediaOutputVolume.Value;

            this.Play(mediaSource);
        }

        public void Resume()
        {
            if (!_paused)
            {
                _mediaPlayer.controls.play();
                _paused = false;
            }
        }
        
        public void SetVolume(int mediaOutputVolumeValue)
        {
            this.Volume = mediaOutputVolumeValue;
        }

        public void SetVolume(IMediaOutputVolume mediaOutputVolume)
        {
            this.Volume = mediaOutputVolume.Value;
        }

        public void Stop()
        {
            _mediaPlayer.controls.stop();
        }
    }
}
 