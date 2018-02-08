using System;
using System.Runtime.CompilerServices;
using Core.Audio;
using Core.Model;
using Quartz;

using Core.Model.Enums;
using Core.Model.Interfaces;

namespace Core.BLL
{
    [DisallowConcurrentExecution]
    public class MediaJob : IJob
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public  MediaJob()
        {
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            
            try
            {
                #region Job Data
                var jsonSerializedCurrentAction = dataMap.GetString("jsonSerializedCurrentAction");
                var jsonSerializedMediaSource = dataMap.GetString("jsonSerializedMediaSource");
                var jsonSerializedMediaOutput = dataMap.GetString("jsonSerializedMediaOutputTarget");
                var jsonSerializedMediaOutputVolume = dataMap.GetString("jsonSerializedMediaOutputVolume");
                var jsonSerializedMediaActionType = dataMap.GetString("jsonSerializedMediaActionType");
                var jsonSerializedDbConnectionStringName = dataMap.GetString("jsonSerializedDbConnectionStringName");

                var currentAction = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.MediaAction>(jsonSerializedCurrentAction);
                var currentMediaSource = Newtonsoft.Json.JsonConvert.DeserializeObject<MediaSource>(jsonSerializedMediaSource);
                var currentMediaOutput = Newtonsoft.Json.JsonConvert.DeserializeObject<MediaOutput>(jsonSerializedMediaOutput);
                var currentMediaOutputVolume = Newtonsoft.Json.JsonConvert.DeserializeObject<MediaOutputVolume>(jsonSerializedMediaOutputVolume);
                var currentMediaActionType = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.Enums.MediaActionTypeOption>(jsonSerializedMediaActionType);
                var dbConnectionStringName = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(jsonSerializedDbConnectionStringName);

                #endregion

                //perform the work we came here for
                this.PerformWork(mediaSource: currentMediaSource, mediaOutput: currentMediaOutput, mediaOutputVolume: currentMediaOutputVolume, mediaActionType: currentMediaActionType);

                //if we reach this point we have succeeded in sending a message to TellstickUnit
                var performedActionsDealer = new PerformedActionsDealer(dbConnectionStringName);
                
                var actionPerformedTime = DateTime.Now;

                //let's register (to db) what we have done
                performedActionsDealer.Register(currentAction, actionPerformedTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool PerformWork(IMediaSource mediaSource, IMediaOutput mediaOutput, IMediaOutputVolume mediaOutputVolume, Model.Enums.MediaActionTypeOption mediaActionType)
        {
            var workPerformed = false;

            switch (mediaActionType)
            {
                case MediaActionTypeOption.Play:
                    Audio.Player.Instance.Play(mediaSource, mediaOutputVolume);
                    workPerformed = true;
                    break;
                case MediaActionTypeOption.Pause:
                    Audio.Player.Instance.Pause();
                    workPerformed = true;
                    break;
                case MediaActionTypeOption.Stop:
                    Audio.Player.Instance.Stop();
                    workPerformed = true;
                    break;
                case MediaActionTypeOption.SetVolume:
                    Audio.Player.Instance.SetVolume(mediaOutputVolume);
                    workPerformed = true;
                    break;

                default:
                    workPerformed = false;
                    throw new ArgumentOutOfRangeException();
            }


            return workPerformed;
        }
    }
}
 