namespace Tellstick.Model.Interfaces
{
    public interface IAction : IEntity
    {
        string CronExpression { get; set; }
    }
}