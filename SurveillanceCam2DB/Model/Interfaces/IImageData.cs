namespace Tellstick.Model.Interfaces
{
    public interface IImageData
    {
        int Image_Id { get; set; }
        bool Active { get; set; }

        byte[] Data { get; set; }
    }
}