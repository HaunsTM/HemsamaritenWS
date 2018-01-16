namespace Core.Model.Interfaces
{
    using System;
    public interface ITellstickAuthentication : IEntity
    {
        int Expires { get; set; }
        string Token { get; set; }

        DateTime Received { get; set; }
    }
}