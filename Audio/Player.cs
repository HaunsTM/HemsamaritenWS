using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Audio
{
    public class Player
    {
        //https://msdn.microsoft.com/en-us/library/windows/desktop/dd562692(v=vs.85).aspx
        private WMPLib.WindowsMediaPlayer _mediaPlayer;

        public Player()
        {
            _mediaPlayer = new WMPLib.WindowsMediaPlayer();
        }

        private void PlayFile(string url)
        {
            _mediaPlayer.URL = url;
            _mediaPlayer.controls.play();
        }
        private void Player_PlayStateChange(int NewState)
        {

            if ((WMPLib.WMPPlayState)NewState == WMPLib.WMPPlayState.wmppsStopped)
            {
            }
        }

        private void Player_MediaError(object pMediaObject)
        {
        }

        public void Bark()
        {
            this.PlayFile(@"https://www.youtube.com/watch/v/XXZ9sohobTk");
        }
    }
}
 