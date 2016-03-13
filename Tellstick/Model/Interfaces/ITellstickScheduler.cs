namespace Tellstick.Model.Interfaces
{
    public interface ITellstickScheduler
    {
        string CronDescription { get; set; }
        string CronExpression { get; set; }
    }
}