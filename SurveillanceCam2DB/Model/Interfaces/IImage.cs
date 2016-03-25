namespace SurveillanceCam2DB.Model.Interfaces
{
    using System;

    public interface IImage : IEntity
    {
        string Description { get; set; }

        long DataLength { get; set; }

        int ImageQualityPercent { get; set; }

        string ImageFormat { get; set; }

        DateTime SnapshotTime { get; set; }
    }
}