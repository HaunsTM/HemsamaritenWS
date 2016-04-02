namespace SurveillanceCam2DB.BLL.Interfaces
{
    public interface IVideoDealerProcessEventArgs
    {
        int Image_Id { get; set; }

        int CurrentImageNumberInSequence { get; set; }

        int TotalNumberOfImagesInSequence { get; set; }
        int PercentDone { get; }
    }
}