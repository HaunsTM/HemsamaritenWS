namespace SurveillanceCam2DB.BLL.Interfaces
{
    public interface IVideoDealerStartUpEventArgs
    {
        string OutputFileName { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int FrameRateMs { get; set; }
    }
}