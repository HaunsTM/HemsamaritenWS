using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Core.BLL.Interfaces;
using Core.Model;
using Core.Model.Interfaces;
using log4net;

namespace Core.BLL
{

    public class MediaActionsDealer
    {

        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string DbConnectionStringName { get; private set; }

        public MediaActionsDealer(string dbConnectionStringName)
        {
            DbConnectionStringName = dbConnectionStringName;
        }

        /// <summary>
        /// Searches for an Action given certain parameters
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns>An Action if it is found, NULL if it is not found</returns>
        public Core.Model.MediaAction ActionExists(IMediaSource mediaSource, IMediaOutput mediaOutput, IMediaOutputVolume mediaOutputVolume, IMediaActionType mediaActionType, IScheduler scheduler)
        {
            using (var db = new Core.Model.HemsamaritenWindowsServiceDbContext(DbConnectionStringName))
            {
                Model.MediaAction actionToSearchFor = null;

                var currentMediaSource = (db.MediaSources.Where(s =>
                    s.Url == mediaSource.Url)).FirstOrDefault();

                var currentMediaOutput = (db.MediaOutputs.Where(o =>
                    o.Target == mediaOutput.Target)).FirstOrDefault();

                var currentMediaOutputVolume = (db.MediaOutputVolumes.Where(oV =>
                    oV.Value == mediaOutputVolume.Value)).FirstOrDefault();

                var currentActionType = (db.MediaActionTypes.Where(aT =>
                    aT.ActionTypeOption == mediaActionType.ActionTypeOption)).FirstOrDefault();

                switch (scheduler == null)
                {
                    case true:
                        actionToSearchFor = (from existingAction in db.Actions.OfType<MediaAction>()
                                             where
                                                 existingAction.Active
                                                 && existingAction.MediaSource.Id == currentMediaSource.Id
                                                 && existingAction.MediaOutput.Id == currentMediaOutput.Id
                                                 && existingAction.MediaOutputVolume.Id == currentMediaOutputVolume.Id
                                                 && existingAction.MediaActionType.Id == currentActionType.Id
                                             select existingAction).FirstOrDefault();
                        break;
                    case false:
                        actionToSearchFor = (from existingAction in db.Actions.OfType<MediaAction>()
                                             where
                                                 existingAction.Active
                                                 && existingAction.MediaSource.Id == currentMediaSource.Id
                                                 && existingAction.MediaOutput.Id == currentMediaOutput.Id
                                                 && existingAction.MediaOutputVolume.Id == currentMediaOutputVolume.Id
                                                 && existingAction.MediaActionType.Id == currentActionType.Id
                                                 && existingAction.Scheduler.Id == scheduler.Id
                                             select existingAction).FirstOrDefault();
                        break;

                }
                return actionToSearchFor;
            }
        }
    }
}
