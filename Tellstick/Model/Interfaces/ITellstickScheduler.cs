﻿namespace Tellstick.Model.Interfaces
{
    public interface ITellstickScheduler : IEntity
    {
        string CronDescription { get; set; }
        string CronExpression { get; set; }
    }
}