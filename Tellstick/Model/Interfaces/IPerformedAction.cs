namespace Tellstick.Model.Interfaces
{
    using System;

    public interface IPerformedAction : IEntity
    {
         DateTime TimeOfPerformance { get; set; }
    }
}