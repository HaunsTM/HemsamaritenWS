using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    public class Player
    {
        private Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;

        private string LibDirectory
        {
            get
            {
                string libDirectory = string.Empty;

                // Use 64 bits library
                libDirectory = Path.Combine(Environment.CurrentDirectory, @"Dependencies\","vlc-2.2.8x64");

                return libDirectory;
            }
        }

        public Player()
        {
            _mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(new DirectoryInfo(this.LibDirectory));
            int i = 7;
        }

        public void Bark()
        {
            int i = 0;
        }
    }
}
