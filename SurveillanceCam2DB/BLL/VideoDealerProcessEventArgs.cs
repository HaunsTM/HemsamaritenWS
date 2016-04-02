namespace SurveillanceCam2DB.BLL
{
    using System;
    using System.Runtime.Serialization;

    using SurveillanceCam2DB.BLL.Interfaces;

    [DataContract]
    public class VideoDealerProcessEventArgs : EventArgs, IVideoDealerProcessEventArgs
    {
        public int Image_Id { get; set; }
        public int CurrentImageNumberInSequence { get; set; }
        public int TotalNumberOfImagesInSequence { get; set; }
        public int PercentDone
        {
            get
            {
                var donePercent = (this.CurrentImageNumberInSequence / this.TotalNumberOfImagesInSequence) * 100;
                return donePercent;
            }
        }
    }
}