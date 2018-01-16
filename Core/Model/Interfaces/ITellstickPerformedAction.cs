namespace Core.Model.Interfaces
{
    using System;

    public interface ITellstickPerformedAction : IEntity
    {
         DateTime Time { get; set; }
    }
}