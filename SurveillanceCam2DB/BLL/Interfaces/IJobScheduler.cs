namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System.Collections.Generic;

    public interface IJobScheduler
    {
        string DbConnectionStringName { get; }

        List<string> CurrentlyExecutingJobsNames { get; }

        void Start();

        void Stop();
    }
}