namespace Tellstick.BLL.Interfaces
{
    public interface IJobScheduler
    {
        string DbConnectionStringName { get; }

        void Start();

        void Stop();
    }
}