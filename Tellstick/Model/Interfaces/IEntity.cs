﻿namespace Tellstick.Model.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }

        bool Active { get; set; }
    }
}