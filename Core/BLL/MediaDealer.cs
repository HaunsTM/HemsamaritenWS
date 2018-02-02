using System;
using System.ComponentModel.DataAnnotations;
using Core.BLL.Interfaces;

namespace Core.BLL
{
    public class MediaDealer : IMediaDealer
    {
        public void SetVolume(int value)
        {
            Core.Audio.Player.Instance.Volume = value;
        }

        public void Play(string url)
        {
            Core.Audio.Player.Instance.Play(url);
        }

        public void Play(string url, int mediaOutputVolume)
        {
            Core.Audio.Player.Instance.Play(url, mediaOutputVolume);
        }

        public void Stop()
        {
            Core.Audio.Player.Instance.Stop();
        }
    }
}
