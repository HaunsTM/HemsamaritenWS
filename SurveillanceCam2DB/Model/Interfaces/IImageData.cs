namespace SurveillanceCam2DB.Model.Interfaces
{
    public interface IImageData
    {
        int Image_Id { get; set; }
        bool Active { get; set; }

        byte[] Data { get; set; }
    }
}