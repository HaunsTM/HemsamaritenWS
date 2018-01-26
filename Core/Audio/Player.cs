using System.ComponentModel.DataAnnotations;
namespace Core.Audio
{
    public class Player
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

        public void Bark()
        {
            this.Volume = 100;
            this.Play(@"http://live-icy.gss.dr.dk/A/A03H.mp3.m3u");
            int i = 0;
        }
    }
}
 