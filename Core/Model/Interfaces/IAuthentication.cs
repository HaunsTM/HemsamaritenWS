namespace Core.Model.Interfaces
{
    using System;
    public interface IAuthentication : IEntity
    {
        int Expires { get; set; }
        string Token { get; set; }

        DateTime Received { get; set; }
    }
}