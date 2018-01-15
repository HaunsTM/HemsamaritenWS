namespace Core.Model.Interfaces
{
    public interface IScheduler : IEntity
    {
        string CronDescription { get; set; }
        string CronExpression { get; set; }
    }
}