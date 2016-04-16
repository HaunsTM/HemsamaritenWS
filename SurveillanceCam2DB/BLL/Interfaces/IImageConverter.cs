namespace SurveillanceCam2DB.BLL.Interfaces
{
    using System.Drawing;
    using System.Drawing.Imaging;

    public interface IImageConverter
    {
        byte[] ImageToByteArray(Image imageIn, ImageFormat format);

        byte[] ImageToByteArray(Image imageIn, ImageFormat format, int imageQualityPercent);

        Image ByteArrayToImage(byte[] byteArrayIn);

        Bitmap ByteArrayToBitmap(byte[] byteArrayIn);
        Bitmap ByteArrayToBitmap(byte[] byteArrayIn, Size newSize);

        byte[] ImageToCompressedByteArray(Image imageIn, ImageFormat format);

        Image CompressedByteArrayToImage(byte[] compressedByteArrayIn);
    }
}