namespace Core.Model.Interfaces
{
    using System;

    public interface IPerformedAction : IEntity
    {
         DateTime Time { get; set; }
    }
}