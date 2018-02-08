using System;
using System.Collections.Generic;

namespace Core.BLL.Interfaces
{
    public interface IPerformedActionsDealer
    {
        string DbConnectionStringName { get; }

        /// <summary>
        /// Registers an occurred TellstickAction to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        bool Register(Model.Action occurredAction, DateTime timeOfOccurrence);
        
        /// <summary>
        /// Retrieves a list of occurred TellstickActions from database
        /// </summary>
        /// <param name="active">A flag that indicates if we should search for active items</param>
        /// <param name="startTime">A time stamp that indicates search start time</param>
        /// <param name="endTime">A time stamp that indicates search end time</param>
        List<Model.Interfaces.IAction> OccurredActions(bool active, DateTime startTime, DateTime endTime);
        
    }
}