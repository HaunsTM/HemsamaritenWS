//Here is the once-per-application setup information
using Core.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using Core.Model.Enums;
using WCF.ServiceLibrary.Interfaces;

namespace WCF.ServiceLibrary
{
    public partial class HemsamaritenService
    {
        void IMediaDuplexService.StartMediaScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    var mediaJobScheduler = new Core.BLL.MediaJobScheduler(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                    mediaJobScheduler.Start();

                    this.SetResponseHttpStatus(HttpStatusCode.OK);
                    log.Debug(String.Format("Started MediaScheduler."));
                }
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                log.Error(String.Format("Failed in starting MediaScheduler."), ex);
            }
        }

        void IMediaDuplexService.StopMediaScheduler()
        {
            try
            {
                lock (_syncRoot)
                {
                    var mediaJobScheduler = new Core.BLL.MediaJobScheduler(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                    mediaJobScheduler.Stop();

                    this.SetResponseHttpStatus(HttpStatusCode.OK);
                    log.Debug(String.Format("Stopped MediaScheduler."));
                }
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                log.Error(String.Format("Failed in stopping MediaScheduler."), ex);
            }
        }
        
        string IMediaDuplexService.SetMediaVolume(int value)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.SetVolume(value);

                returnMessage = String.Format("Set volume to = {0}", value.ToString());
                this.SetResponseHttpStatus(HttpStatusCode.OK);

                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                returnMessage = String.Format("Failed in setting volume to = {0}. Reason: {1}", value.ToString(), ex.Message);
            }
            return returnMessage;
        }

        string IMediaDuplexService.PlayMedia(string url)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Play(url);

                returnMessage = String.Format("Playing {0}.", url);
                this.SetResponseHttpStatus(HttpStatusCode.OK);

                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                returnMessage = String.Format("Failed in playing {0}. Reason {1}", url, ex.Message);
            }
            return returnMessage;
        }

        string IMediaDuplexService.PlayMediaAndSetVolume(string url, int mediaOutputVolume)
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Play(url, mediaOutputVolume);

                returnMessage = String.Format("Playing {0} with volume {1}.", url, mediaOutputVolume.ToString());
                this.SetResponseHttpStatus(HttpStatusCode.OK);

                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                returnMessage = String.Format("Failed in playing {0} with volume {1}. Reason {2}", url, mediaOutputVolume.ToString(), ex.Message);
            }
            return returnMessage;
        }

        string IMediaDuplexService.StopMediaPlay()
        {
            var returnMessage = "";
            try
            {
                var mediaDealer = new Core.BLL.MediaDealer();
                mediaDealer.Stop();

                returnMessage = String.Format("Stop");
                this.SetResponseHttpStatus(HttpStatusCode.OK);

                log.Debug(returnMessage);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                returnMessage = String.Format("Failed in stopping. Reason: {0}", ex.Message);
            }
            return returnMessage;
        }

        List<RegisteredMediaSource> IMediaDuplexService.AllMediaSourcesList()
        {
            var presetMediaSources = new List<RegisteredMediaSource>();
            try
            {
                var mediaSourceDealer = new Core.BLL.MediaSourceDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                presetMediaSources = mediaSourceDealer.PredefinedMediaSourcesList();

                this.SetResponseHttpStatus(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                log.Error($"Failed in getting AllMediaSourcesList", ex);
            }
            return presetMediaSources;
        }

        List<RegisteredMediaSource> IMediaDuplexService.InternetStreamRadioSourcesList()
        {
            var presetMediaSources = new List<RegisteredMediaSource>();
            try
            {
                var mediaSourceDealer = new Core.BLL.MediaSourceDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                presetMediaSources = mediaSourceDealer.PredefinedMediaSourcesListBy(MediaCategoryType.InternetStreamRadio);

                this.SetResponseHttpStatus(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                log.Error($"Failed in getting InternetStreamRadioSourcesList", ex);
            }
            return presetMediaSources;
        }

        List<RegisteredMediaSource> IMediaDuplexService.SoundEffectSourcesList()
        {
            var presetMediaSources = new List<RegisteredMediaSource>();
            try
            {
                var mediaSourceDealer = new Core.BLL.MediaSourceDealer(DB_CONN_HEMSAMARITEN_WINDOWS_SERVICE);
                presetMediaSources = mediaSourceDealer.PredefinedMediaSourcesListBy(MediaCategoryType.FileSoundEffect);

                this.SetResponseHttpStatus(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                this.SetResponseHttpStatus(HttpStatusCode.BadRequest);
                log.Error($"Failed in getting SoundEffectSourcesList", ex);
            }

            return presetMediaSources;
        }
    }
}
