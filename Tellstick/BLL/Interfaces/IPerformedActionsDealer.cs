namespace Tellstick.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IPerformedActionsDealer
    {
        string DbConnectionStringName { get; }

        /// <summary>
        /// Registers an occurred TellstickAction to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        bool Register(Tellstick.Model.Action occurredAction, DateTime timeOfOccurrence);

        /// <summary>
        /// Registers an occurred TellstickAction to database
        /// </summary>
        /// <param name="occurredAction"></param>
        /// <param name="timeOfOccurrence"></param>
        /// <returns></returns>
        bool Register(int occurredAction_Id, DateTime timeOfOccurrence);

        /// <summary>
        /// Retrieves a list of occurred TellstickActions from database
        /// </summary>
        /// <param name="active">A flag that indicates if we should search for active items</param>
        /// <param name="startTime">A time stamp that indicates search start time</param>
        /// <param name="endTime">A time stamp that indicates search end time</param>
        List<Model.Action> OccurredTellstickActions(bool active, DateTime startTime, DateTime endTime);
    }
}