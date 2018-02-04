using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model.Enums;

namespace Core.Model
{
    public sealed class DefaultDataDbInitializerHelper
    {
        public HemsamaritenWindowsServiceDbContext _context;

        public DefaultDataDbInitializerHelper(HemsamaritenWindowsServiceDbContext context)
        {
            _context = context;
        }

        public TellstickAction TellstickActionToSave(bool active, string tellstickUnitName, TellstickActionTypeOption actionTypeOption, string cronExpression)
        {
            var tellstickAction = new TellstickAction();

            var scheduler = _context.Schedulers
                .Where(s => s.Active)
                .Where(s => s.CronExpression == cronExpression)
                .FirstOrDefault<Scheduler>();

            var tellstickActionType= _context.TellstickActionTypes
                .Where(s => s.Active)
                .Where(s => s.ActionTypeOption == actionTypeOption)
                .FirstOrDefault<TellstickActionType>();

            var tellstickUnit = _context.TellstickUnits
                .Where(u => u.Active)
                .Where(u => u.Name == tellstickUnitName)
                .FirstOrDefault<TellstickUnit>();

            tellstickAction.Active = active;
            tellstickAction.Scheduler = scheduler;
            tellstickAction.TellstickActionType = tellstickActionType;
            tellstickAction.TellstickUnit = tellstickUnit;

            return tellstickAction;
        }

        public List<TellstickAction> TellstickActionsToSave(List<Tuple<bool, string, TellstickActionTypeOption, string>> tellstickActionData)
        {
            var tellstickActionsToSave = new List<TellstickAction>();
            foreach (var data in tellstickActionData)
            {
                var tellstickActionActiveStatus = data.Item1;
                var tellstickUnitName = data.Item2;
                var actionTypeOption = data.Item3;
                var cronExpression = data.Item4;

                var tellstickActionToSave =  TellstickActionToSave(active: tellstickActionActiveStatus, tellstickUnitName: tellstickUnitName,
                    actionTypeOption: actionTypeOption, cronExpression: cronExpression);

                tellstickActionsToSave.Add(tellstickActionToSave);
            }
            return tellstickActionsToSave;
        }

        public MediaAction MediaActionToSave(string cronExpression, bool active, string mediaSourceName, MediaActionTypeOption? mediaActionTypeOption, MediaOutputTargetType? mediaOutputTargetType, MediaOutputVolumeValue? mediaOutputVolumeLabel)
        {
            var mediaAction = new MediaAction();

            mediaAction.Active = active;

            if (cronExpression != null)
            {
                var scheduler = _context.Schedulers
                    .Where(s => s.Active)
                    .Where(s => s.CronExpression == cronExpression)
                    .FirstOrDefault<Scheduler>();
                mediaAction.Scheduler = scheduler;
            }

            if (mediaSourceName != null)
            {
                var mediaSource = _context.MediaSources
                    .Where(s => s.Active)
                    .Where(s => s.Name == mediaSourceName)
                    .FirstOrDefault<MediaSource>();
                mediaAction.MediaSource = mediaSource;
            }
            if (mediaActionTypeOption != null)
            {
                var mediaActionType = _context.MediaActionTypes
                    .Where(s => s.Active)
                    .Where(s => s.ActionTypeOption == mediaActionTypeOption)
                    .FirstOrDefault<MediaActionType>();
                mediaAction.MediaActionType = mediaActionType;
            }
            if (mediaOutputTargetType != null)
            {
                var mediaOutput = _context.MediaOutputs
                    .Where(s => s.Active)
                    .Where(s => s.Target == mediaOutputTargetType)
                    .FirstOrDefault<MediaOutput>();
                mediaAction.MediaOutput = mediaOutput;
            }
            if (mediaOutputVolumeLabel != null)
            {
                var mediaOutputVolume = _context.MediaOutputVolumes
                    .Where(s => s.Active)
                    .Where(s => s.Label == mediaOutputVolumeLabel)
                    .FirstOrDefault<MediaOutputVolume>();
                mediaAction.MediaOutputVolume = mediaOutputVolume;
            }

            return mediaAction;
        }

        public List<MediaAction> MediaActionsToSave(List<Tuple<string, bool, string, MediaActionTypeOption, MediaOutputTargetType?, MediaOutputVolumeValue?>> mediaActionData)
        {
            var mediaActionsToSave = new List<MediaAction>();
            foreach (var data in mediaActionData)
            {
                var cronExpression = data.Item1;
                var active = data.Item2;
                var mediaSourceName = data.Item3;
                var mediaActionTypeOption = data.Item4;
                var mediaOutputTargetType = data.Item5;
                var mediaOutputVolumeLabel = data.Item6;

                var mediaActionToSave = MediaActionToSave(cronExpression, active, mediaSourceName, mediaActionTypeOption, mediaOutputTargetType, mediaOutputVolumeLabel);

                mediaActionsToSave.Add(mediaActionToSave);
            }
            return mediaActionsToSave;
        }
    }
}