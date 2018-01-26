using System.ComponentModel.DataAnnotations;
using AudioSwitcher.AudioApi.CoreAudio;

namespace Core.Audio
{
    public class SystemVolumeConfigurator : ISystemVolumeConfigurator
    {
        private CoreAudioDevice _defaultPlaybackDevice;
        
        public SystemVolumeConfigurator()
        {
            _defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }

        [Range(0, 100,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Volume
        {
            get
            {
                return _defaultPlaybackDevice.Volume;
            }
            set
            {
                _defaultPlaybackDevice.Volume = value;
            }
        }
    }
}
